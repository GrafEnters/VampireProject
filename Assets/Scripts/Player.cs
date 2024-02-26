using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Player : CreatureBase {
    public static Player CurrentPlayer;

    public ConstructingManager ConstructingManager => GetComponent<ConstructingManager>();

    public Inventory GetInventory() => GetComponent<InventoryComponent>().Inventory;

    protected override void Awake() {
        base.Awake();
        CurrentPlayer = this;
        UIFactory.ChangeCursorState(true);
        AddAction("Die", ShowDeathDialog);
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
                UIFactory.ShowDialog(DialogType.BuildingSelection, data);
            }
        }
    }

    private void ShowDeathDialog(Object data) {
        DeathDialogData dialogData = new DeathDialogData {
            Player = this
        };
        UIFactory.ShowDialog(DialogType.DeathDialog, dialogData);
    }
}