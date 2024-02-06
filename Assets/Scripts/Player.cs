using System;
using DefaultNamespace;
using UnityEngine;

public class Player : ComponentsContainer {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            if (InventoryDialog.isActive) {
                UIFactory.CloseInventory();
            } else {
                var i = GetComponent<InventoryComponent>();
                UIFactory.ShowInventory(i.Inventory);
            }
        }
    }
}