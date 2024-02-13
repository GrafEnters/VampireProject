using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingsTable", fileName = "BuildingsTable", order = 3)]
public class BuildingsList : ScriptableObject {
    public List<BuildingConfig> Buildings;
}