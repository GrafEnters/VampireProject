using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ResourceCollectorComponent : InventoryComponent {
    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody.transform.CompareTag("Resource")) {
            ResourceBase res = other.attachedRigidbody.GetComponent<ResourceBase>();

            Inventory.AddResource(res.Resource);
            Destroy(other.attachedRigidbody.gameObject);
        }
    }
}