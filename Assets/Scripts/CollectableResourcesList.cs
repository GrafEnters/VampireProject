using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CollectableResourcesTable", fileName = "CollectableResourcesTable", order = 4)]
public class CollectableResourcesList : ScriptableObject {
    public List<ResourceBase> Resources;
}