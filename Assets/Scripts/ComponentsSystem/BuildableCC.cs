using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuildableCC : ComponentsContainer, IBuildable {
    private List<Collider> _nonTriggerColliders;
    
    private Dictionary<MeshRenderer, Material> _meshes;
    private Dictionary<GameObject, int> _layers;
    private int _layer;

    public void SetConstructingState() {
        _layer = gameObject.layer;
        ChangeLayer(LayerMask.NameToLayer("Ignore Raycast"));
        if (_nonTriggerColliders == null) {
            _nonTriggerColliders = GetComponentsInChildren<Collider>().Where(c => !c.isTrigger).ToList();
        }

        foreach (var col in _nonTriggerColliders) {
            col.isTrigger = true;
        }
    }

    public void ChangeMaterial(Material newMaterial) {
        if (_meshes == null) {
            _meshes = new Dictionary<MeshRenderer, Material>();
            foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>()) {
                _meshes.Add(meshRenderer, meshRenderer.material);
            }
        }
        
        foreach (MeshRenderer meshRenderer in _meshes.Keys) {
            meshRenderer.material = newMaterial;
        }
    }

    public void ResetMaterial() {
        foreach (var kvp in _meshes) {
            kvp.Key.material = kvp.Value;
        }
    }

    public void SetFixedState() {
        ChangeLayer(_layer);
        foreach (var col in _nonTriggerColliders) {
            col.isTrigger = false;
        }
    }

    private void ChangeLayer(int layer) {
        if (_layers == null) {
            _layers = new Dictionary<GameObject, int>();
            foreach (Transform transform in GetComponentsInChildren<Transform>()) {
                var go = transform.gameObject;
                _layers.Add(go, go.layer);
            }
        }

        foreach (Transform layers in transform.GetComponentsInChildren<Transform>()) {
            layers.gameObject.layer = layer;
        }
    }
    
    public void ResetLayers() {
        foreach (var kvp in _layers) {
            kvp.Key.layer = kvp.Value;
        }
    }
}