using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDialog : DialogBase {
    [SerializeField]
    private List<InventoryItemView> _slots;

   
    private Action<Resource> _onClickedResource;
    private Inventory _inventory;
    
    public static bool isActive = false;
    public void Show() {
        isActive = true;
        gameObject.SetActive(isActive);
    }
    
    public void Hide() {
        isActive = false;
        gameObject.SetActive(isActive);
    }

    private void OnClickedResource(Resource res) {
        _onClickedResource?.Invoke(res);
    }
    
    public override void Set(DialogDataBase dialogDataBase) {
        base.Set(dialogDataBase);
        InventoryDialogData data = (InventoryDialogData)dialogDataBase;
        _inventory = data.Player;
        UpdateSlots();
    }
    public void Set(Inventory inventory) {
        _inventory = inventory;
        UpdateSlots();
    }

    public void UpdateSlots() {
        foreach (InventoryItemView view in _slots) {
            view.Clear();
        }

        int i = 0;
        foreach (InventorySlot slot in _inventory.Slots) {
            _slots[i].Set(slot.Resource,OnClickedResource);
            i++;
        }
    }

    public void SetOnClickedResource(Action<Resource> onClickedResource) {
        _onClickedResource = onClickedResource;
    }
}

public class InventoryDialogData : DialogDataBase {
    public Inventory Player;
}