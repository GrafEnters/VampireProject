using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {
    public class ComponentsContainer : MonoBehaviour {
        private readonly Dictionary<string, Action> _nameActionDictionary = new Dictionary<string, Action>();

        protected virtual void Awake() {
            InitComponents();
        }

        private void InitComponents() {
            foreach (ComponentBase component in GetComponents<ComponentBase>()) {
                component.Init(AddAction, ReceiveAction);
            }
        }

        private void AddAction(string type, Action action) {
            if (_nameActionDictionary.ContainsKey(type)) {
                _nameActionDictionary[type] += action;
            } else {
                _nameActionDictionary.Add(type, action);
            }
        }

        private void ReceiveAction(string type) {
            if (_nameActionDictionary.ContainsKey(type)) {
                _nameActionDictionary[type].Invoke();
            }
        }
    }
}