using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ResourceCollectorComponent : InventoryComponent {
    private void OnCollisionEnter(Collision collision) {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb == null) {
            return;
        }
        if (rb.transform.CompareTag("Resource")) {
            ResourceBase res = rb.GetComponent<ResourceBase>();

            Inventory.AddResource(res.Resource);
            Destroy(rb.gameObject);
        }
    }

    /*
    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody.transform.CompareTag("Resource")) {
            ResourceBase res = other.attachedRigidbody.GetComponent<ResourceBase>();

            Inventory.AddResource(res.Resource);
            Destroy(other.attachedRigidbody.gameObject);
        }
    }*/
}