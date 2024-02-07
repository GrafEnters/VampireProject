using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryComponent))]
public class LootCreatorComponent : ComponentBase {
    [SerializeField]
    private List<Resource> _lootResources;

    private void Awake() {
        CreateLoot();
    }

    private void CreateLoot() {
        InventoryComponent inv = GetComponent<InventoryComponent>();
        foreach (Resource res in _lootResources) {
            inv.Inventory.AddResource(res);
        }
    }
}