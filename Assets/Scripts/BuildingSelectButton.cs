using System;
using TMPro;
using UnityEngine;

public class BuildingSelectButton : MonoBehaviour {

    [SerializeField]
    private TMP_Text _nameText;
    private Action<string> _onCLick;
    private string _buildingType;

    public void Set(string buildingId,Action<string> onCLick) {
        _nameText.text = buildingId;
        _buildingType = buildingId;
        _onCLick = onCLick;
    }

    public void OnClick() {
        _onCLick?.Invoke(_buildingType);
    }
}