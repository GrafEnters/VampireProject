using System;
using UnityEngine;

public class InventoryComponent : ComponentBase {
    [SerializeField]
    private int _slotsAmount;
    public Inventory Inventory;

    private void Awake() {
        Inventory = new Inventory(_slotsAmount);
    }
}