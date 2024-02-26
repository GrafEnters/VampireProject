using System;
using UnityEngine;

public class UIFactory : MonoBehaviour {
    [SerializeField]
    private Transform _dialogsHolder;

    [SerializeField]
    private InventoryDialog _inventoryDialog;

    [SerializeField]
    private ChestInteractionDialog _chestInteractionDialog;

    [SerializeField]
    private FurnaceInteractionDialog _furnaceInteractionDialog;

    [SerializeField]
    private BuildingSelectionDialog _buildingSelectionDialog;

    [SerializeField]
    private DeathDialog _deathDialog;

    private static UIFactory factory;

    private void Awake() {
        factory = this;
    }

    public static void ChangeCursorState(bool isLocked) {
        Cursor.visible = !isLocked;
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public static bool IsInDialog() {
        return factory._inventoryDialog.isActive || factory._chestInteractionDialog.isActive || factory._furnaceInteractionDialog.isActive ||
               factory._buildingSelectionDialog.isActive;
    }

    public static void ShowDialog(DialogType type, DialogDataBase data = null) {
        var factory = FindObjectOfType<UIFactory>();
        switch (type) {
            case DialogType.Inventory:
                factory._inventoryDialog.Set(data);
                factory._inventoryDialog.Show();
                break;
            case DialogType.ChestInteraction:
                factory._chestInteractionDialog.Set(data);
                factory._chestInteractionDialog.Show();
                break;
            case DialogType.FurnaceInteraction:
                factory._furnaceInteractionDialog.Set(data);
                factory._furnaceInteractionDialog.Show();
                break;
            case DialogType.BuildingSelection:
                factory._buildingSelectionDialog.Set(data);
                factory._buildingSelectionDialog.Show();
                break;
            case DialogType.DeathDialog:
                factory._deathDialog.Set(data);
                factory._deathDialog.Show();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        ChangeCursorState(false);
    }

    public static void HideDialog(DialogType type) {
        var factory = FindObjectOfType<UIFactory>();
        switch (type) {
            case DialogType.Inventory:

                factory._inventoryDialog.Hide();
                break;
            case DialogType.ChestInteraction:

                factory._chestInteractionDialog.Hide();
                break;
            case DialogType.FurnaceInteraction:

                factory._furnaceInteractionDialog.Hide();
                break;
            case DialogType.BuildingSelection:

                factory._buildingSelectionDialog.Hide();
                break;
            case DialogType.DeathDialog:

                factory._deathDialog.Hide();
                break;
        }

        ChangeCursorState(true);
    }
}

public enum DialogType {
    Inventory = 0,
    ChestInteraction = 1,
    FurnaceInteraction = 2,
    BuildingSelection = 3,
    DeathDialog = 4,
}

public class DialogDataBase { }