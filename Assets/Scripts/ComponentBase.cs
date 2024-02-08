using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ComponentBase : MonoBehaviour {
    protected Action<string, Object> _sendAction;
    protected ComponentsContainer _container;
    
    public void Init(ComponentsContainer container, Action<string, Action<Object>> addAction, Action<string, Object> onSendAction) {
        _container = container;
        Init(addAction, onSendAction);
    }
    protected virtual void Init(Action<string, Action<Object>> addAction, Action<string, Object> onSendAction) {
        _sendAction = onSendAction;
    }
}