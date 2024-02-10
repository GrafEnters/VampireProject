using UnityEngine;
using Object = System.Object;

[RequireComponent(typeof(InventoryComponent))]
public class ChestCC : BuildableCC, ICreateDialogData {
    public DialogDataBase GetDialogData(Object obj = null) {
        Player player = obj as Player;
        InventoryComponent invComponent = GetComponent<InventoryComponent>();
        return new ChestInteractionDialogData() {
            Player = player.GetInventory(),
            Chest = invComponent.Inventory
        };
    }
}