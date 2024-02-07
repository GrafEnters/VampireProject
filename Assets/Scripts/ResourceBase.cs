using System;
using UnityEngine;

public class ResourceBase : MonoBehaviour {
    [SerializeField]
    private Resource _resource;

    public Resource Resource => _resource;
}