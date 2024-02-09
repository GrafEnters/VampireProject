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

    public static void ShowInventory(Inventory inventory) {
        var factory = FindObjectOfType<UIFactory>();
        
        factory._inventoryDialog.Set(inventory);
        factory._inventoryDialog.Show();
    }
    
    public static void CloseInventory() {
        var factory = FindObjectOfType<UIFactory>();

        factory._inventoryDialog.Hide();
    }

    public static void ShowDialog(DialogType type, DialogDataBase data = null) {
        var factory = FindObjectOfType<UIFactory>();
        switch (type ) {
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
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}

public enum DialogType {
    Inventory = 0,
    ChestInteraction = 1,
    FurnaceInteraction = 2
}

public class DialogDataBase {
    
}