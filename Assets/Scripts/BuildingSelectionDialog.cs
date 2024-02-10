using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectionDialog : DialogBase {
    [SerializeField]
    private BuildingSelectButton _buttonPrefab;

    [SerializeField]
    private List<BuildingSelectButton> _selectButtons;

    [SerializeField]
    private Transform _buttonsHolder;

    public static bool StaticActive = false;

    private void Start() {
        foreach (object enumStr in Enum.GetValues(typeof(BuildingType))) {
            BuildingSelectButton button = Instantiate(_buttonPrefab, _buttonsHolder);
            button.Set((BuildingType)enumStr, SelectBuilding);
        }
    }

    public override void Hide() {
        base.Hide();
        StaticActive = false;
    }

    public override void Show() {
        base.Show();
        StaticActive = true;
    }

    public void SelectBuilding(BuildingType type) {
        IBuildable buildable = BuildingFactory.GetPrefabByType(type);
    }
}

public enum BuildingType {
    Foundament = 0,
    Wall = 1,
}