using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ClickableComponent : ComponentBase {
    private void OnMouseUpAsButton() {
        _sendAction?.Invoke("Click", Player.CurrentPlayer);
    }
}