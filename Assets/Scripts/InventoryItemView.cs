using System;
using TMPro;
using UnityEngine;

public class InventoryItemView : MonoBehaviour {
    [SerializeField]
    private TMP_Text _nameText, _amountText;

    private Action<Resource> _onClicked;
    private Resource _resource;

    public void Clear() {
        _nameText.text = "";
        _amountText.text = "";
    }
    public void Set(Resource res,Action<Resource> onClick) {
        _resource = res;
        _onClicked = onClick;
        _nameText.text = res.Name;
        _amountText.text = res.Amount.ToString();
    }

    public void OnClicked() {
        if (_resource == null) {
            return;
        }
        
        if (_resource.Amount == 0) {
            return;
        }
        Resource tmp = _resource.Clone;
        
        
        if (Input.GetKey(KeyCode.LeftControl)) {
            tmp.Amount = 1;
        } else if (Input.GetKey(KeyCode.LeftShift)) {
            if (tmp.Amount > 1) {
                tmp.Amount /= 2;
            }
        }

        _onClicked?.Invoke(tmp);
    }
}