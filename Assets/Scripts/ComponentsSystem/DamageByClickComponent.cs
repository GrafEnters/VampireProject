using System;
using UnityEngine;

namespace DefaultNamespace {
    [RequireComponent(typeof(HpComponent))]
    [RequireComponent(typeof(ClickableComponent))]
    public class DamageByClickComponent : ComponentBase {
        [SerializeField]
        private int _damagePerClick = 1;

        public override void Init(Action<string, Action> addAction, Action<string> onSendAction) {
            base.Init(addAction, onSendAction);
            addAction.Invoke("Click", TakeDamage);
        }

        private void TakeDamage() {
            GetComponent<HpComponent>().TakeDamage(_damagePerClick);
        }
    }
}