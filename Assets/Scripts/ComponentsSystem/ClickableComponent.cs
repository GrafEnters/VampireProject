using System;
using UnityEngine;

namespace DefaultNamespace {
    [RequireComponent(typeof(Rigidbody))]
    public class ClickableComponent : ComponentBase {
        private void OnMouseUpAsButton() {
            _sendAction?.Invoke("Click");
        }
    }
}