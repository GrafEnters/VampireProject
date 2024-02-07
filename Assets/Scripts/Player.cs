using System;
using UnityEngine;

public class Player : ComponentsContainer {

    public static Player CurrentPlayer;

    public Inventory GetInventory() => GetComponent<InventoryComponent>().Inventory;
    
    protected override void Awake() {
        CurrentPlayer = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            if (InventoryDialog.isActive) {
                UIFactory.CloseInventory();
            } else {
                Inventory inventory = GetInventory();
                UIFactory.ShowInventory(inventory);
            }
        }
    }
}