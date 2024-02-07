using UnityEngine;

public class UIFactory : MonoBehaviour {
    [SerializeField]
    private Transform _dialogsHolder;

    [SerializeField]
    private InventoryDialog _inventoryDialog;
    
    [SerializeField]
    private ChestInteractionDialog _chestInteractionDialog;

    public static void ShowInventory(Inventory inventory) {
        var factory = FindObjectOfType<UIFactory>();
        
        factory._inventoryDialog.Set(inventory);
        factory._inventoryDialog.Show();
    }
    
    public static void CloseInventory() {
        var factory = FindObjectOfType<UIFactory>();

        factory._inventoryDialog.Hide();
    }
    
    public static void ShowChestInteraction( Inventory player,Inventory chest) {
        var factory = FindObjectOfType<UIFactory>();
        
        factory._chestInteractionDialog.Set(player,chest);
        factory._chestInteractionDialog.Show();
    }
    
    public static void CloseChestInteraction() {
        var factory = FindObjectOfType<UIFactory>();

        factory._chestInteractionDialog.Hide();
    }
}