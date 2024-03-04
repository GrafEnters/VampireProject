using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class ComponentBase : MonoBehaviour {
    protected Action<ComponentAction, Object> _sendAction;
    protected ComponentsContainer _container;
    
    public void Init(ComponentsContainer container, Action<ComponentAction, Action<Object>> addAction, Action<ComponentAction, Object> onSendAction) {
        _container = container;
        Init(addAction, onSendAction);
    }
    protected virtual void Init(Action<ComponentAction, Action<Object>> addAction, Action<ComponentAction, Object> onSendAction) {
        _sendAction = onSendAction;
    }
}

[Serializable]
public enum ComponentAction {
    None = 0,
    Click = 1,
    Interact = 2,
    TakeDamage = 3,
    Die = 4
}