using UnityEngine;

public class ResourceBase : MonoBehaviour {
    [SerializeField]
    private string _resourceName;

    [SerializeField]
    private int _amount;

    public string ResourceName => _resourceName;
    public int Amount => _amount;
}