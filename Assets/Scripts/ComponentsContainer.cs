using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ComponentsContainer : MonoBehaviour {
    private readonly Dictionary<string, Action<Object>> _nameActionDictionary = new Dictionary<string, Action<Object>>();

    protected virtual void Awake() {
        InitComponents();
    }

    private void InitComponents() {
        foreach (ComponentBase component in GetComponents<ComponentBase>()) {
            component.Init(AddAction, ReceiveAction);
        }
    }

    private void AddAction(string type, Action<Object> action) {
        if (_nameActionDictionary.ContainsKey(type)) {
            _nameActionDictionary[type] += action;
        } else {
            _nameActionDictionary.Add(type, action);
        }
    }

    private void ReceiveAction(string type, Object data) {
        if (_nameActionDictionary.ContainsKey(type)) {
            _nameActionDictionary[type].Invoke(data);
        }
    }
}