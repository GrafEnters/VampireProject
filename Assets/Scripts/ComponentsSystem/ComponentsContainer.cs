using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ComponentsContainer : MonoBehaviour {
    private readonly Dictionary<ComponentAction, Action<Object>> _nameActionDictionary = new Dictionary<ComponentAction, Action<Object>>();

    protected virtual void Awake() {
        InitComponents();
    }

    private void InitComponents() {
        foreach (ComponentBase component in GetComponents<ComponentBase>()) {
            component.Init(this, AddAction, ReceiveAction);
        }
    }

    protected void AddAction(ComponentAction type, Action<Object> action) {
        if (_nameActionDictionary.ContainsKey(type)) {
            _nameActionDictionary[type] += action;
        } else {
            _nameActionDictionary.Add(type, action);
        }
    }

    private void ReceiveAction(ComponentAction type, Object data) {
        if (_nameActionDictionary.ContainsKey(type)) {
            _nameActionDictionary[type].Invoke(data);
        }
    }
}