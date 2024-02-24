using System;
using UnityEngine;

public class ResourceBase : MonoBehaviour {
    [SerializeField]
    private SerializableResource _resource;

    public Resource Resource => _resource.Resource;

    public void SetResourceAmount(int amount) {
        _resource.Amount = amount;
    }
}

public enum ResourceType {
    Empty = 0,
    Wood = 1,
    Stone = 2,
    Fiber = 3,
    IronOre = 4,
    IronIngot = 5,
    Wool = 6,
    Meat = 7,
    Bone = 8,
}