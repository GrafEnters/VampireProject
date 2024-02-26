using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour {
    [SerializeField]
    private float _spawnRadius = 5.0f;

    [SerializeField]
    private float _respawnDelay = 10.0f;

    [SerializeField]
    private float _checkCooldown = 5.0f;

    [SerializeField]
    private int _resourcesAmount = 5;

    [SerializeField]
    private List<ComponentsContainer> _resources = new List<ComponentsContainer>();

    private void Start() {
        if (transform.childCount == 0) {
            PopulateArea();
        }
        StartCoroutine(CheckResources());
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }

    private void PopulateArea() {
        for (int i = 0; i < _resourcesAmount; i++) {
            SpawnResource();
        }
    }

    public void RepopulateArea() {
        DestroyChildren();
        PopulateArea();
    }

    private void DestroyChildren() {
        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[transform.childCount];
        int i = 0;
        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }
    

    private IEnumerator CheckResources() {
        while (true) {
            yield return new WaitForSeconds(_checkCooldown);
            if (transform.childCount < _resourcesAmount) {
                yield return new WaitForSeconds(_respawnDelay);
                SpawnResource();
            }
        }
    }

    private void SpawnResource() {
        //TODO add pooling
        Vector3 spawnRayPosition = transform.position +
                                   new Vector3(Random.Range(-_spawnRadius, _spawnRadius), _spawnRadius,
                                       Random.Range(-_spawnRadius, _spawnRadius));
        if (Physics.Raycast(spawnRayPosition, Vector3.down, out RaycastHit hit, _spawnRadius * 2, LayerMask.GetMask("Ground"))) {
            Vector3 randomRot = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            ComponentsContainer resource = _resources[Random.Range(0, _resources.Count)];
            Instantiate(resource, hit.point, Quaternion.LookRotation(randomRot, hit.normal), transform);
        }
    }
}
#if UNITY_EDITOR

[CustomEditor(typeof(SpawnArea))]
[CanEditMultipleObjects]
public class SpawnAreaEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        SpawnArea spawnArea = (SpawnArea) target;
        GUILayout.Space(20);
        if (GUILayout.Button("Repopulate area")) {
            spawnArea.RepopulateArea();
        }
    }
}

#endif