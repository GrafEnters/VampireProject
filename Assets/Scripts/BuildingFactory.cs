using System;
using UnityEngine;

public class BuildingFactory : MonoBehaviour {
    [SerializeField]
    private Transform _buildingsContainer;

    [SerializeField]
    private BuildableCC _foundamentCc, _wall, _furnace, _chest,_stairs;

    //TODO Add pooling
    public static BuildableCC GetPrefabByType(BuildingType type) {
        var factory = FindObjectOfType<BuildingFactory>();
        switch (type) {
            case BuildingType.Foundament:
                return Instantiate(factory._foundamentCc, factory._buildingsContainer);

            case BuildingType.Wall:
                return Instantiate(factory._wall, factory._buildingsContainer);
            case BuildingType.Furnace:
                return Instantiate(factory._furnace, factory._buildingsContainer);
            case BuildingType.Chest:
                return Instantiate(factory._chest, factory._buildingsContainer);
            case BuildingType.Stairs:
                return Instantiate(factory._stairs, factory._buildingsContainer);

            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}