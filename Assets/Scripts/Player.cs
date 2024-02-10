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
            if (InventoryDialog.StaticActive) {
                UIFactory.HideDialog(DialogType.Inventory);
            } else {
                InventoryDialogData data = new InventoryDialogData {
                    Player = GetInventory()
                };

                UIFactory.ShowDialog(DialogType.Inventory, data);
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            if (BuildingSelectionDialog.StaticActive) {
                UIFactory.HideDialog(DialogType.BuildingSelection);
            } else {
                UIFactory.ShowDialog(DialogType.BuildingSelection);
            }
        }
    }
}