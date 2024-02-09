using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory {
    private List<InventorySlot> _slots;

    public List<InventorySlot> Slots => _slots;

    public Inventory(int amount) {
        _slots = new List<InventorySlot>(amount);
        for (int i = 0; i < amount; i++) {
            _slots.Add(new InventorySlot());
        }
    }

    public bool HasResource(Resource res) {
        return _slots.Any(r => r.CheckResource(res));
    }

    public InventorySlot FindResourceSlot(Resource res) {
        return _slots.Find(r => r.CheckResource(res));
    }

    public void AddResource(Resource res) {
        if (HasResource(res)) {
            FindResourceSlot(res).AddResource(res);
        } else {
            if (HasResource(Resource.Empty)) {
                FindResourceSlot(Resource.Empty).AddResource(res);
            }
        }
    }

    public void RemoveResourceAmount(Resource res) {
        if (HasResource(res)) {
            InventorySlot curResSlot = FindResourceSlot(res);
            curResSlot.RemoveResource(res);
        } else {
            Debug.LogError($"No such resource to remove: {res.Type}");
        }
    }
}