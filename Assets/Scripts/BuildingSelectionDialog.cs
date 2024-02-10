using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectionDialog : DialogBase {
    [SerializeField]
    private BuildingSelectButton _buttonPrefab;

    [SerializeField]
    private List<BuildingSelectButton> _selectButtons;

    [SerializeField]
    private Transform _buttonsHolder;

    public static bool StaticActive = false;
    private Player _player;

    private void Start() {
        foreach (object enumStr in Enum.GetValues(typeof(BuildingType))) {
            BuildingSelectButton button = Instantiate(_buttonPrefab, _buttonsHolder);
            button.Set((BuildingType)enumStr, SelectBuilding);
        }
    }

    public override void Set(DialogDataBase dialogDataBase) {
        base.Set(dialogDataBase);
        _player = (dialogDataBase as BuildingSelectionDialogData).Player;
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
        BuildableCC buildable = BuildingFactory.GetPrefabByType(type);
        _player.ConstructingManager.StartConstructing(buildable);
        Hide();
    }
}

public class BuildingSelectionDialogData : DialogDataBase {
    public Player Player;
}

public enum BuildingType {
    Foundament = 0,
    Wall = 1,
    Furnace = 2,
    Chest = 3,
    Stairs = 4
}