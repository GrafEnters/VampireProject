using UnityEngine;

public class CollectableResourceFactory : MonoBehaviour {
    [SerializeField]
    private Transform _collectableResourcesContainer;

    [SerializeField]
    private CollectableResourcesList _resources;

    public static CollectableResourcesList ResourcesList => FindObjectOfType<CollectableResourceFactory>()._resources;

    //TODO Add pooling
    public static ResourceBase GetPrefabByType(ResourceType type) {
        var factory = FindObjectOfType<CollectableResourceFactory>();
        ResourceBase rb = factory._resources.Resources.Find(b => b.Resource.Type == type);
        if (rb == null) {
            Debug.LogError($"Resource not found in list, type: {type.ToString()}");
            return null;
        }

        return Instantiate(rb, factory._collectableResourcesContainer);
    }
}