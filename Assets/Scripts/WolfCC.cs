using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WolfCC : CreatureBase {
    [SerializeField]
    private FieldOfView _fov;

    [SerializeField]
    private NavMeshAgent _navAgent;

    private Vector3 _target;
    private GameObject _targetObj;

    protected override void Awake() {
        _fov.Init(IsImportant, OnSeeObj, OnRemoveObj);
        InvokeRepeating(nameof(UpdateTarget), 0.5f, 0.5f);
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
        }
    }

    private void OnRemoveObj(GameObject obj) {
        if (obj == _targetObj) {
            _targetObj = null;
        }
    }

    private void UpdateTarget() {
        if (_navAgent.isStopped) {
            return;
        }

        if (_targetObj != null) {
            _target = _targetObj.transform.position;
            _navAgent.SetDestination(_target);
        }

        if ((_targetObj.transform.position - transform.position).magnitude <= 0.5f) {
            OnReachDestination();
        }
    }

    private void OnReachDestination() {
        if (_targetObj.CompareTag("Player")) {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack() {
        _navAgent.isStopped = true;
        Debug.Log($"{gameObject.name} attacked player!");
        yield return new WaitForSeconds(1);
        _navAgent.isStopped = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_target, Vector3.one * 0.5f);
    }
}