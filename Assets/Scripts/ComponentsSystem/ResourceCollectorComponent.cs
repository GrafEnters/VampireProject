using UnityEngine;

namespace DefaultNamespace {
    [RequireComponent(typeof(Rigidbody))]
    public class ResourceCollectorComponent : InventoryComponent {
        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody.transform.CompareTag("Resource")) {
                var res = other.attachedRigidbody.GetComponent<ResourceBase>();

                Inventory.AddResource(res.ResourceName, res.Amount);
                Destroy(other.gameObject);
            }
        }
    }
}