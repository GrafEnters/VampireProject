using UnityEngine;

public class FurnaceInteractionDialog : ChestInteractionDialog {
    [SerializeField]
    private InventoryItemView _fuelSlot, _smeltingSlot, _resultSlot;

    private FurnaceCC _furnaceCc;

    public override void Set(DialogDataBase dialogDataBase) {
        base.Set(dialogDataBase);
        FurnaceInteractionDialogData data = (FurnaceInteractionDialogData)dialogDataBase;
        _furnaceCc = data.Furnace;
        _furnaceCc.OnTriggerUpdateView += TriggeredUpdateView;
        _playerInventory.Set(data.Player);
        _playerInventory.SetOnClickedResource(delegate(Resource resource) { MoveToFurnace(resource, data.Player, data.Chest); });
        _chestInventory.Set(data.Chest);
        _chestInventory.SetOnClickedResource(delegate(Resource resource) { MoveResource(resource, data.Chest, data.Player); });
        _fuelSlot.Set(_furnaceCc.Fuel.Resource, delegate(Resource resource) {
            _furnaceCc.Fuel.RemoveResource(resource);
            data.Player.AddResource(resource);
            TriggeredUpdateView(_furnaceCc);
        });
        _smeltingSlot.Set(_furnaceCc.Smelting.Resource, delegate(Resource resource) {
            _furnaceCc.Smelting.RemoveResource(resource);
            data.Player.AddResource(resource);
            TriggeredUpdateView(_furnaceCc);
        });
        UpdateRecipeView();
    }

    public override void Hide() {
        base.Hide();
        _furnaceCc.OnTriggerUpdateView -= TriggeredUpdateView;
    }

    private void TriggeredUpdateView(ComponentsContainer cc) {
        _playerInventory.UpdateSlots();
        _chestInventory.UpdateSlots();
        _fuelSlot.UpdateView(_furnaceCc.Fuel.Resource);
        _smeltingSlot.UpdateView(_furnaceCc.Smelting.Resource);
        _resultSlot.UpdateView(_furnaceCc.GetCurrentRecipeResult);
    }

    protected override void MoveResource(Resource resource, Inventory from, Inventory to) {
        base.MoveResource(resource, from, to);
        UpdateRecipeView();
    }

    private void MoveToFurnace(Resource resource, Inventory from, Inventory to) {
        if (SmeltingRecipe.GetFuelAmount(resource) > 0) {
            if (_furnaceCc.Fuel.CanAddResource(resource)) {
                MoveToFuel(resource, from);
                return;
            }
        }

        if (SmeltingRecipe.GetRecipe(resource).Result != Resource.Empty) {
            if (_furnaceCc.Smelting.CanAddResource(resource)) {
                MoveToSmelting(resource, from);
                UpdateRecipeView();
                return;
            }
        }

        MoveResource(resource, from, to);
    }

    private void MoveToFuel(Resource resource, Inventory from) {
        from.RemoveResourceAmount(resource);
        _furnaceCc.Fuel.AddResource(resource);
        _playerInventory.UpdateSlots();
        _fuelSlot.UpdateView(_furnaceCc.Fuel.Resource);
    }

    private void MoveToSmelting(Resource resource, Inventory from) {
        from.RemoveResourceAmount(resource);
        _furnaceCc.Smelting.AddResource(resource);
        _playerInventory.UpdateSlots();
        _smeltingSlot.UpdateView(_furnaceCc.Smelting.Resource);
    }

    private void UpdateRecipeView() {
        _resultSlot.UpdateView(_furnaceCc.GetCurrentRecipeResult);
    }
}

public class FurnaceInteractionDialogData : ChestInteractionDialogData {
    public FurnaceCC Furnace;
}