using UnityEngine;
using Object = System.Object;

[RequireComponent(typeof(InventoryComponent))]
public class FurnaceCC : ComponentsContainer, ICreateDialogData {

    private float _fuelSecondsLeft;

    public DialogDataBase GetDialogData(Object obj = null) {
        Player player = obj as Player;
        InventoryComponent invComponent = GetComponent<InventoryComponent>();
        return  new FurnaceInteractionDialogData() {
            Furnace = this,
            Player = player.GetInventory(),
            Chest = invComponent.Inventory
        };
        
    }
}


public class SmeltingRecipe {
    
}
