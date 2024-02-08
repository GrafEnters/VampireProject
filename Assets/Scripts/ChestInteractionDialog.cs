using UnityEngine;

public class ChestInteractionDialog : DialogBase {
    [SerializeField]
    private InventoryDialog _playerInventory, _chestInventory;

    public static bool isActive = false;

    public override void Set(DialogDataBase dialogDataBase) {
        base.Set(dialogDataBase);
        ChestInteractionDialogData data = (ChestInteractionDialogData)dialogDataBase;
        
        _playerInventory.Set(data.Player);
        _playerInventory.SetOnClickedResource(
            delegate(Resource resource) { MoveResorce(resource, data.Player, data.Chest); });
        _chestInventory.Set(data.Chest);
        _chestInventory.SetOnClickedResource(
            delegate(Resource resource) { MoveResorce(resource, data.Chest, data.Player); });
    }

    public void Show() {
        isActive = true;
        gameObject.SetActive(isActive);
    }

    public void Hide() {
        isActive = false;
        gameObject.SetActive(isActive);
    }

    public void Set(Inventory player, Inventory chest) {
        _playerInventory.Set(player);
        _playerInventory.SetOnClickedResource(
            delegate(Resource resource) { MoveResorce(resource, player, chest); });
        _chestInventory.Set(chest);
        _chestInventory.SetOnClickedResource(
            delegate(Resource resource) { MoveResorce(resource, chest, player); });
    }

    private void MoveResorce(Resource resource, Inventory from, Inventory to) {
        Resource tmp = resource.Clone;
        from.RemoveResourceAmount(tmp);
        to.AddResource(tmp);
        _playerInventory.UpdateSlots();
        _chestInventory.UpdateSlots();
    }
}

public class ChestInteractionDialogData : DialogDataBase {
    public Inventory Player;
    public Inventory Chest;
}
