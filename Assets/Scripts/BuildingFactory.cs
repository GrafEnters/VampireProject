using UnityEngine;

public class BuildingFactory : MonoBehaviour {
    [SerializeField]
    private Transform _buildingsContainer;

    [SerializeField]
    private BuildingsList _buildings;

    public static BuildingsList BuildingsList => FindObjectOfType<BuildingFactory>()._buildings;

    //TODO Add pooling
    public static BuildableCC GetPrefabByType(string buildingUniqueName) {
        var factory = FindObjectOfType<BuildingFactory>();
        BuildingConfig b = factory._buildings.Buildings.Find(b => b.BuildingUniqueName == buildingUniqueName);
        if (b == null) {
            Debug.LogError($"Building not found in list, id: {buildingUniqueName}");
            return null;
        }

        return Instantiate(b.Building, factory._buildingsContainer);
    }
}