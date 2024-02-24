using System.Linq;
using UnityEngine;

public class ConstructingManager : MonoBehaviour {
    private BuildableCC _building;
    private Player _player;
    private Coroutine _constructingCoroutine;
    private bool _isBuilding;

    [SerializeField]
    private LayerMask _raycastLayerMask;

    [SerializeField]
    private Material _constructingInProgress, _constructingUnavailable;

    [SerializeField]
    private float _maxDist = 5, _rotationSpeed = 1;


    
    
    

    public void StartConstructing(BuildableCC building) {
        if (_building != null) {
            QuitConstructing();
        }

        _building = building;
        _isBuilding = true;

        _building.SetConstructingState();
        _building.ChangeMaterial(_constructingInProgress);
    }

    private void Update() {
        if (!_isBuilding) {
            return;
        }
        
        _building.transform.position = CalculatePos();
        _building.transform.position += CalculateAnchorShift();
        if (Input.mouseScrollDelta != Vector2.zero) {
            _building.transform.Rotate(Vector3.up, Input.mouseScrollDelta.y * _rotationSpeed);
        }

        if (Input.GetMouseButtonDown(1)) {
            QuitConstructing();
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            FixContructableAndEndConstructing();
            return;
        }
    }

    private Vector3 CalculatePos() {
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width, Screen.height) / 2);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxDist, _raycastLayerMask)) {
            return hitInfo.point;
        }

        return ray.GetPoint(_maxDist);
    }
    
    private Vector3 CalculateAnchorShift() {
        foreach (var variable in _building.Anchors) {
            Vector3 possibleShift = variable.TryShift();
            if (possibleShift != Vector3.zero) {
                return possibleShift;
            }
        }
        return Vector3.zero;
    }

    private void FixContructableAndEndConstructing() {
        if (!_isBuilding) {
            return;
        }

        _isBuilding = false;
        _building.SetFixedState();
        _building.ResetMaterial();
        _building = null;
    }

    private void QuitConstructing() {
        if (!_isBuilding) {
            return;
        }

        _isBuilding = false;
    }
}