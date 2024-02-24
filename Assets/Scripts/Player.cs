using System;
using UnityEngine;

public class Player : CreatureBase {

   
    
    public static Player CurrentPlayer;

    public ConstructingManager ConstructingManager => GetComponent<ConstructingManager>();

    public Inventory GetInventory() => GetComponent<InventoryComponent>().Inventory;
    
    protected override void Awake() {
        CurrentPlayer = this;
        UIFactory.ChangeCursorState(true);
    }

    private void Update() {
       
        
        if (UIFactory.IsInDialog()) {
            return;
        }
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
                BuildingSelectionDialogData data = new BuildingSelectionDialogData {
                    Player = this
                };
                UIFactory.ShowDialog(DialogType.BuildingSelection,data);
            }
        }
    }
}