using UnityEngine;

public class ChestInteractionDialog : DialogBase {
    [SerializeField]
    protected InventoryDialog _playerInventory, _chestInventory;

    public override void Set(DialogDataBase dialogDataBase) {
        base.Set(dialogDataBase);
        ChestInteractionDialogData data = (ChestInteractionDialogData)dialogDataBase;

        _playerInventory.Set(data.Player);
        _playerInventory.SetOnClickedResource(delegate(Resource resource) { MoveResource(resource, data.Player, data.Chest); });
        _chestInventory.Set(data.Chest);
        _chestInventory.SetOnClickedResource(delegate(Resource resource) { MoveResource(resource, data.Chest, data.Player); });
    }

    protected virtual void MoveResource(Resource resource, Inventory from, Inventory to) {
        from.RemoveResourceAmount(resource);
        to.AddResource(resource);
        _playerInventory.UpdateSlots();
        _chestInventory.UpdateSlots();
    }
}

public class ChestInteractionDialogData : DialogDataBase {
    public Inventory Player;
    public Inventory Chest;
}