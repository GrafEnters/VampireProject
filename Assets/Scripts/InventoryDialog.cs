using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class InventoryDialog : MonoBehaviour {
    [SerializeField]
    private List<InventoryItemView> _slots;

    public static bool isActive = false;

    public void Show() {
        isActive = true;
        gameObject.SetActive(isActive);
    }
    
    public void Hide() {
        isActive = false;
        gameObject.SetActive(isActive);
    }
    
    public void Set(Inventory _inventory) {
        foreach (var VARIABLE in _slots) {
            VARIABLE.Clear();
        }

        int i = 0;
        foreach (var VARIABLE in _inventory._resources) {
            _slots[i].Set(VARIABLE.Key, VARIABLE.Value.ToString());
            i++;
        }
    }
}