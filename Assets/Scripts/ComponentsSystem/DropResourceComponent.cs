using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class DropResourceComponent : ComponentBase {

    [SerializeField]
    private ResourceBase _resourceBase;
    protected  override void Init(Action<string, Action<Object>> addAction, Action<string,Object> onSendAction) {
        base.Init(addAction, onSendAction);
        addAction.Invoke("Die", DropResource);
    }

    private void DropResource(Object data) {
        //TODO Rework via ResourceFactory
        Instantiate(_resourceBase, transform.position, transform.rotation);
    }
}