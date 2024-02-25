using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WolfCC : CreatureBase {
    [SerializeField]
    private float _reachDistance = 0.75f;

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

    protected override void Awake() {
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

        while ((_target - _rb.position).magnitude > _reachDistance) {
            Vector3 dir = (_target - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dir);

            var targetDir = _target - transform.position;
            var forward = transform.forward;
            var localTarget = transform.InverseTransformPoint(_target);

            angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

            Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * _ikSettingsConfig.RotationSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);

            _rb.MovePosition(_rb.position + dir * _ikSettingsConfig.MoveSpeed * Time.fixedDeltaTime);
            Vector3 newRaycastsPoint = _footRaycastsOffset + dir * _ikSettingsConfig.RotationSpeed;
            newRaycastsPoint.y = 0.4f;
            _footRaycasts.transform.position = transform.position + newRaycastsPoint * _ikSettingsConfig.RaycastDistanceParam;
            yield return new WaitForFixedUpdate();

            if (_targetObj != null) {
                _target = _targetObj.transform.position;
                _headTarget.transform.position = _target;
            }
        }

        OnReachDestination();
    }

    private IEnumerator LookAround() {
        Debug.Log($"{gameObject.name} looking around!");
        float time = 0;
        float maxTime = 0.5f;
        var rb = GetComponent<Rigidbody>();
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
        Debug.Log($"{gameObject.name} attacking!");
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