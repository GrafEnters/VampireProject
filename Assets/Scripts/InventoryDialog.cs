using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDialog : MonoBehaviour {
    [SerializeField]
    private List<InventoryItemView> _slots;

    public static bool isActive = false;
    private Action<Resource> _onClickedResource;
    private Inventory _inventory;
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
    
    public void Set(Inventory inventory) {
        _inventory = inventory;
        UpdateSlots();
    }

    public void UpdateSlots() {
        foreach (InventoryItemView view in _slots) {
            view.Clear();
        }

        int i = 0;
        foreach (Resource res in _inventory.Resources) {
            _slots[i].Set(res,OnClickedResource);
            i++;
        }
    }

    public void SetOnClickedResource(Action<Resource> onClickedResource) {
        _onClickedResource = onClickedResource;
    }
}