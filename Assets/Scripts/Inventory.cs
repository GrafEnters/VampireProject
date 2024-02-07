using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory {

    [SerializeField]
    private List<Resource> _resources = new List<Resource>();

    public List<Resource> Resources => _resources;

    public bool HasResource(Resource res) {
        return _resources.Any(r => r.Name == res.Name);
    }
    public Resource FindResource(string name) {
        return _resources.Find(r => r.Name == name);
    }
    
    public void AddResource(Resource res) {
        if (HasResource(res)) {
            FindResource(res.Name).Amount += res.Amount;
        } else {
            _resources.Add(res);
        }
    }

    public void RemoveResourceAmount(Resource res) {
        if (HasResource(res)) {
            var curRes = FindResource(res.Name);
            curRes.Amount  -= res.Amount;
            if ( curRes.Amount  < 0) {
                Debug.LogError($"Resource amount is less then 0: {curRes.Name}, amount:{curRes.Amount}");
            }

            if (curRes.Amount == 0) {
                _resources.Remove(curRes);
            }
        } else {
            Debug.LogError($"No such resource to remove: {res.Name}");
        }
    }
    
    public void RemoveResource(Resource res) {
        if (HasResource(res)) {
            Resource curRes = FindResource(res.Name);
            _resources.Remove(curRes);
        } else {
            Debug.LogError($"No such resource to remove: {res.Name}");
        }
    }
    
}