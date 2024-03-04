public class InteractableComponent : ComponentBase {
    public void Interact() {
        _sendAction?.Invoke(ComponentAction.Interact, Player.CurrentPlayer);
    }
}