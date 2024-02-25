using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
    private List<GameObject> _objectsInSight = new List<GameObject>();
    private List<GameObject> _trackedObjects = new List<GameObject>();
    public List<GameObject> TrackedObjects => _trackedObjects; 
    private Func<GameObject, bool> _isImportant;
    private Action<GameObject> _onAdded;
    private Action<GameObject> _onRemoved;

    public void Init(Func<GameObject, bool> isImportant, Action<GameObject> OnAdded, Action<GameObject> OnRemoved) {
        _isImportant = isImportant;
        _onAdded = OnAdded;
        _onRemoved = OnRemoved;
    }

    private void Start() {
        InvokeRepeating(nameof(UpdateTrackedObjects), 1, 1);
    }

    public void ResetList() {
        _objectsInSight = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.attachedRigidbody) {
            return;
        }
        GameObject go = other.attachedRigidbody.gameObject;

        if (_isImportant == null || !_isImportant(go)) {
            return;
        }

        if (!_objectsInSight.Contains(go)) {
            _objectsInSight.Add(go);
            _onAdded?.Invoke(go);
        }
    }

    private void UpdateTrackedObjects() {
        _trackedObjects = new List<GameObject>();
        LayerMask msk = LayerMask.GetMask("Default","Ground","Player");
        foreach (GameObject objInSight in _objectsInSight) {
            Ray ray = new Ray(transform.position, objInSight.transform.position - transform.position);
            if (!Physics.SphereCast(ray,0.1f, out RaycastHit hit,msk )) {
                //TODO with more raycasts to it will be able to detect objects more precise
                continue;
            }

            if (hit.collider.attachedRigidbody != null && hit.collider.attachedRigidbody.gameObject == objInSight) {
                _trackedObjects.Add(objInSight);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!other.attachedRigidbody) {
            return;
        }
        GameObject go = other.attachedRigidbody.gameObject;
        if (_objectsInSight.Contains(go)) {
            _objectsInSight.Remove(go);
            _onRemoved?.Invoke(go);
        }
    }

    private void OnDrawGizmosSelected() {
        foreach (GameObject objInSight in _objectsInSight) {
            Vector3 position = objInSight.transform.position;
            Gizmos.DrawWireCube(position, objInSight.transform.lossyScale);
            Gizmos.DrawLine(transform.position,position);
        }
        Gizmos.color = Color.yellow;
        foreach (GameObject trackedObj in _trackedObjects) {
            Gizmos.DrawWireCube(trackedObj.transform.position, trackedObj.transform.lossyScale);
        }
    }
}