using UnityEngine;

public class FurnaceInteractionDialog : ChestInteractionDialog {
    [SerializeField]
    private InventoryItemView _fuelSlot, _cookingSlot, _resultSlot;
}

public class FurnaceInteractionDialogData : ChestInteractionDialogData {
    public FurnaceCC Furnace;
}
