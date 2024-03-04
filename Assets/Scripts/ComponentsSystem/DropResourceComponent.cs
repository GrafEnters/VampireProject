using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class DropResourceComponent : ComponentBase {

    [SerializeField]
    private List<SerializableResource> _resourcesToDrop;
    
    [SerializeField]
    private float _randomDropRadius = 0.25f;
    protected  override void Init(Action<ComponentAction, Action<Object>> addAction, Action<ComponentAction,Object> onSendAction) {
        base.Init(addAction, onSendAction);
        addAction.Invoke(ComponentAction.Die, DropResource);
    }

    private void DropResource(Object data) {
        //TODO Rework via ResourceFactory
        foreach (var resource in _resourcesToDrop) {
            var resBase = CollectableResourceFactory.GetPrefabByType(resource.Type);
            Vector3 rndDropRadius = new Vector3(Random.Range(-_randomDropRadius, _randomDropRadius), 0,
                Random.Range(-_randomDropRadius, _randomDropRadius));
            resBase.transform.position = transform.position + rndDropRadius;
            resBase.transform.rotation = transform.rotation;
            resBase.SetResourceAmount(  resource.Resource.Amount);
        }
    }
}