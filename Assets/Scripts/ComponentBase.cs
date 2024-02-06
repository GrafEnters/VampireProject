using System;
using UnityEngine;

namespace DefaultNamespace {
    public class ComponentBase : MonoBehaviour {
        protected Action<string> _sendAction;

        public virtual void Init(Action<string, Action> addAction, Action<string> onSendAction) {
            _sendAction = onSendAction;
        }
    }
}