using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ComponentBase : MonoBehaviour {
    protected Action<string, Object> _sendAction;

    public virtual void Init(Action<string, Action<Object>> addAction, Action<string, Object> onSendAction) {
        _sendAction = onSendAction;
    }
}