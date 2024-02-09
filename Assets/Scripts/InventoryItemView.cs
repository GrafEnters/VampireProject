using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour {
    [SerializeField]
    private TMP_Text _nameText, _amountText;

    [SerializeField]
    private Image _backImage;
    
    private Action<Resource> _onClicked;
    private Resource _resource;

    public void Clear() {
        _nameText.text = "";
        _amountText.text = "";
    }
    public void Set(Resource res,Action<Resource> onClick) {
        _onClicked = onClick;
        UpdateView(res);
    }

    public void UpdateView(Resource res) {
        if (res == Resource.Empty) {
            _nameText.text = "";
            _amountText.text = "";
            _backImage.color = Color.grey;
            return;
        }
        _resource = res;
        _nameText.text = res.Type.ToString();
        _amountText.text = res.Amount.ToString();
        _backImage.color = Color.white;
    }
    

    public void OnClicked() {
        if (_resource.Amount == 0) {
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl)) {
            _resource.Amount = 1;
        } else if (Input.GetKey(KeyCode.LeftShift)) {
            if (_resource.Amount > 1) {
                _resource.Amount /= 2;
            }
        }

        _onClicked?.Invoke(_resource);
    }
}