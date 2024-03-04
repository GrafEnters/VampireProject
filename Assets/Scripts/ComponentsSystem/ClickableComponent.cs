using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ClickableComponent : ComponentBase {
    private void OnMouseUpAsButton() {
        if (UIFactory.IsInDialog()) {
            return;
        }

        _sendAction?.Invoke(ComponentAction.Click, Player.CurrentPlayer);
    }
}