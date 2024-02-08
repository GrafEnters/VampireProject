using System;
using UnityEngine;

public class ResourceBase : MonoBehaviour {
    [SerializeField]
    private Resource _resource;

    public Resource Resource => _resource;
}

public enum ResourceType {
    Wood = 0,
    Stone = 1,
    Fiber = 2,
    IronOre = 3,
    IronIngot = 4
}