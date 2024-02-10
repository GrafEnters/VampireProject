using System;
using UnityEngine;
using Object = System.Object;

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
    

    public static void ShowDialog(DialogType type, DialogDataBase data = null) {
        var factory = FindObjectOfType<UIFactory>();
        switch (type) {
            case DialogType.Inventory:
                factory._chestInteractionDialog.Set(data);
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
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
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
        }
    }
}

public enum DialogType {
    Inventory = 0,
    ChestInteraction = 1,
    FurnaceInteraction = 2,
    BuildingSelection = 3
}

public class DialogDataBase {
    
}