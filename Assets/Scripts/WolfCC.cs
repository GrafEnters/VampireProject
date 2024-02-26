using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WolfCC : CreatureBase {
    [SerializeField]
    private float _reachDistance = 0.75f;

    [SerializeField]
    private Animator _animator;


    [SerializeField]
    private FieldOfView _fov;

    [SerializeField]
    private NavMeshAgent _navAgent;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private IkSettingsConfig _ikSettingsConfig;

    private Vector3 _target;
    private GameObject _targetObj;
    private Coroutine _taskCoroutine;

    [SerializeField]
    private GameObject _ikTargetsContainer;

    [SerializeField]
    private Transform _headTarget;

    [SerializeField]
    private Transform _footRaycasts;

    [SerializeField]
    private Vector3 _footRaycastsOffset;

    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private static readonly int Velocity = Animator.StringToHash("Velocity");

    protected override void Awake() {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        _fov.Init(IsImportant, OnSeeObj, OnRemoveObj);
        InvokeRepeating(nameof(UpdateTarget), 0.05f, 0.05f);
        InitIkBones();
    }

    private void InitIkBones() {
        _ikTargetsContainer.transform.SetParent(null);
    }

    private bool IsImportant(GameObject obj) {
        if (obj == gameObject) {
            return false;
        }

        if (obj.GetComponent<CreatureBase>()) {
            return true;
        }

        return false;
    }

    private void OnSeeObj(GameObject obj) {
        if (obj.tag == "Player") {
            _targetObj = obj;
            StopTask();
        }
    }

    private void OnRemoveObj(GameObject obj) {
        if (obj == _targetObj) {
            _targetObj = null;
        }
    }

    private void UpdateTarget() {
        if (_taskCoroutine != null) {
            return;
        }

        if (_targetObj != null) {
            _target = _targetObj.transform.position;
            _headTarget.transform.position = _target;
            _taskCoroutine = StartCoroutine(Hunt());
        }
    }

    private void OnReachDestination() {
        if (_targetObj == null) {
            _taskCoroutine = StartCoroutine(LookAround());
            return;
        }

        if (_targetObj.CompareTag("Player")) {
            _taskCoroutine = StartCoroutine(Attack());
        }
    }

    private IEnumerator Hunt() {
        Debug.Log($"{gameObject.name} hunting!");
        Vector3 velocity = Vector3.zero;
        while ((_target - _rb.position).magnitude > _reachDistance) {
            Vector3 dir = (_target - transform.position).normalized;
            dir.y = 0;
            float angle = Vector3.Angle(transform.forward, dir);
            
            
            Vector3 localTarget = transform.InverseTransformPoint(_target);
            angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
            Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * _ikSettingsConfig.RotationSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);

            /* Physics
                Vector3 forcedStep = dir * _ikSettingsConfig.StepForce;
                if (Vector3.Angle(_rb.velocity, forcedStep) > _ikSettingsConfig.MaxAngleBeforeStoppingCoefficient) {
                    forcedStep *= _ikSettingsConfig.StoppingCoefficient;
                }
            
                _rb.AddForce( forcedStep * _ikSettingsConfig.MoveSpeed * Time.fixedDeltaTime);

                if (_rb.velocity.magnitude >= _ikSettingsConfig.MaxRbVelocity) {
                    _rb.velocity = _rb.velocity.normalized * _ikSettingsConfig.MaxRbVelocity;
                }
            }*/

            Vector3 shift = dir * _ikSettingsConfig.MoveSpeed * Time.fixedDeltaTime;
            velocity += shift;
            if (velocity.magnitude > _ikSettingsConfig.MaxMoveSpeed) {
                velocity = velocity.normalized * _ikSettingsConfig.MaxMoveSpeed;
            }

            _animator.SetFloat(Velocity, velocity.magnitude / _ikSettingsConfig.MaxMoveSpeed);
            _rb.MovePosition(_rb.position + velocity);

            Vector3 newRaycastsPoint = _footRaycastsOffset + dir * _ikSettingsConfig.RaycastDistanceParam;
            newRaycastsPoint.y = 0.4f;
            _footRaycasts.transform.position = transform.position + newRaycastsPoint;

            if (_targetObj != null) {
                _target = _targetObj.transform.position;
                _headTarget.transform.position = _target;
            }
            
            yield return new WaitForFixedUpdate();
        }

        OnReachDestination();
    }

    private IEnumerator LookAround() {
        Debug.Log($"{gameObject.name} looking around!");
        float time = 0;
        float maxTime = 0.5f;
        while (time < maxTime) {
            _headTarget.transform.position = transform.position + Quaternion.Euler(0, 90 * (time / maxTime), 0) * transform.forward * 5;
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
        }

        time = 0;
        maxTime = 1f;
        while (time < maxTime) {
            _headTarget.transform.position = transform.position + Quaternion.Euler(0, 90 - 180 * (time / maxTime), 0) * transform.forward * 5;
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
        }

        time = 0;
        maxTime = 0.5f;
        while (time < maxTime) {
            _headTarget.transform.position = transform.position + Quaternion.Euler(0, -90 + 90 * (time / maxTime), 0) * transform.forward * 5;
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
        }
        /*
        time = 0;
        maxTime = 1f;
        while (time < 1) {
            Vector3 euler = rb.rotation.eulerAngles;
            euler.y += 360 * Time.fixedDeltaTime;
            rb.MoveRotation(Quaternion.Euler(euler));
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
        }*/

        //_navAgent.enabled = true;
    }

    private IEnumerator Attack() {
        _navAgent.enabled = false;
        _animator.SetTrigger(Attack1);
        yield return new WaitForSeconds(1);
        StopTask();
        //_navAgent.enabled = true;
    }

    private void StopTask() {
        if (_taskCoroutine != null) {
            StopCoroutine(_taskCoroutine);
            _taskCoroutine = null;
            //  _navAgent.enabled = true;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_target, Vector3.one * 0.5f);
    }

    private void OnDestroy() {
        Destroy(_ikTargetsContainer);
    }
}