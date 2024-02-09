using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryComponent))]
public class LootCreatorComponent : ComponentBase {
    [SerializeField]
    private List<SerializableResource> _lootResources;

    private void Start() {
        CreateLoot();
    }

    private void CreateLoot() {
        InventoryComponent inv = GetComponent<InventoryComponent>();
        foreach (SerializableResource res in _lootResources) {
            inv.Inventory.AddResource(res.Resource);
        }
    }
}