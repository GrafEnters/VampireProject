using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public readonly Dictionary<string, int> _resources = new Dictionary<string, int>();

    public void AddResource(string type, int amount) {
        if (_resources.ContainsKey(type)) {
            _resources[type] += amount;
        } else {
            _resources.Add(type,amount);
        }
    }

    public void RemoveResource(string type, int amount) {
        if (_resources.ContainsKey(type)) {
            _resources[type] -= amount;
            if (_resources[type] < 0) {
                Debug.LogError($"Resource amount is less then 0: {type}, amount:{_resources[type]}");
            }

            if (_resources[type] == 0) {
                _resources.Remove(type);
            }
        } else {
            Debug.LogError($"No such resource to remove: {type}");
        }
    }
    
}