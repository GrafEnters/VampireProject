using UnityEngine;

public class InteractionChecker {
   
    
    private const float RAY_RADIUS = 0.2f;
    private const float MAX_INTERACTION_DISTANCE = 5;

    private GameObject _seenObject;
    
    public void UpdateSeenObject() {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height) / 2);
        LayerMask mask = LayerMask.GetMask("Default", "Ground", "Resource");
        if (Physics.Raycast(ray, out RaycastHit hitInfo, MAX_INTERACTION_DISTANCE, mask)) {
            var rb  = hitInfo.collider.attachedRigidbody;
            _seenObject = rb != null ? rb.gameObject : null;
        } else {
            _seenObject = null;
        }

        if (_seenObject == null) {
            if (Physics.SphereCast(ray, RAY_RADIUS, out hitInfo, MAX_INTERACTION_DISTANCE, mask)) {
                var rb  = hitInfo.collider.attachedRigidbody;
                _seenObject = rb != null ? rb.gameObject : null;
            } else {
                _seenObject = null;
            }
        }
        

        UIFactory.UpdateInteractableNotification(CheckInteractableObject() != null);
    }

    public ClickableComponent CheckClickableObject() {
        if (_seenObject == null) {
            return null;
        }

        return _seenObject.GetComponent<ClickableComponent>();
    }
    
    public InteractableComponent CheckInteractableObject() {
        if (_seenObject == null) {
            return null;
        }

        return _seenObject.GetComponent<InteractableComponent>();
    }
}