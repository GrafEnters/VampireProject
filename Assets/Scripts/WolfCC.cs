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

    private Vector3 _target;
    private GameObject _targetObj;
    private Coroutine _taskCoroutine;

    protected override void Awake() {
        _fov.Init(IsImportant, OnSeeObj, OnRemoveObj);
        InvokeRepeating(nameof(UpdateTarget), 0.3f, 0.3f);
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
        if (!_navAgent.enabled) {
            return;
        }

        if (_targetObj != null) {
            _target = _targetObj.transform.position;
            _navAgent.SetDestination(_target);
        }

        if ((_navAgent.destination - transform.position).magnitude <= _reachDistance) {
            OnReachDestination();
        }
    }

    private void OnReachDestination() {
        if (_targetObj == null) {
            _taskCoroutine = StartCoroutine(LookAround());
            return;
        }

        if (_targetObj.CompareTag("Player")) {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator LookAround() {
      
        _navAgent.enabled = false;
        float time = 0;
        var rb = GetComponent<Rigidbody>();
        while (time < 1) {
            Vector3 euler = rb.rotation.eulerAngles;
            euler.y += 360 * Time.fixedDeltaTime;
            rb.MoveRotation(Quaternion.Euler(euler));
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
        }

        Debug.Log($"{gameObject.name} looking around!");
        yield return new WaitForSeconds(1);
       
        _navAgent.enabled = true;
    }

    private IEnumerator Attack() {
      
        _navAgent.enabled = false;
        Debug.Log($"{gameObject.name} attacked player!");
        yield return new WaitForSeconds(1);

        _navAgent.enabled = true;
    }

    private void StopTask() {
        if (_taskCoroutine != null) {
            StopCoroutine(_taskCoroutine);
        
            _navAgent.enabled = true;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_target, Vector3.one * 0.5f);
    }
}