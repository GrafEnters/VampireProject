using System;
using TMPro;
using UnityEngine;

public class BuildingSelectButton : MonoBehaviour {

    [SerializeField]
    private TMP_Text _nameText;
    private Action<BuildingType> _onCLick;
    private BuildingType _buildingType;

    public void Set(BuildingType type,Action<BuildingType> onCLick) {
        _nameText.text = type.ToString();
        _buildingType = type;
        _onCLick = onCLick;
    }

    public void OnClick() {
        _onCLick?.Invoke(_buildingType);
    }
}