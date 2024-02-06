using UnityEngine;

public class UIFactory : MonoBehaviour {
    [SerializeField]
    private Transform _dialogsHolder;

    [SerializeField]
    private InventoryDialog _inventoryDialog;

    public static void ShowInventory(Inventory inventory) {
        var factory = FindObjectOfType<UIFactory>();
        
        factory._inventoryDialog.Set(inventory);
        factory._inventoryDialog.Show();
    }
    
    public static void CloseInventory() {
        var factory = FindObjectOfType<UIFactory>();

        factory._inventoryDialog.Hide();
    }
}