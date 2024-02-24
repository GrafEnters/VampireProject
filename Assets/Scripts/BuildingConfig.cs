using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/BuildingConfig", fileName = "BuildingConfig", order = 2)]

public class BuildingConfig : ScriptableObject {

    public string BuildingUniqueName = "";
    
    public BuildableCC Building;

}