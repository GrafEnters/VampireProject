using System;
using UnityEngine;

namespace DefaultNamespace {
    public class DropResourceComponent : ComponentBase {

        [SerializeField]
        private ResourceBase _resourceBase;
        public override void Init(Action<string, Action> addAction, Action<string> onSendAction) {
            base.Init(addAction, onSendAction);
            addAction.Invoke("Die", DropResource);
        }

        private void DropResource() {
            //TODO Rework via ResourceFactory
            Instantiate(_resourceBase, transform.position, transform.rotation);
        }
    }
}