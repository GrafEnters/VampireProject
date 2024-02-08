using System;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(HpComponent))]
[RequireComponent(typeof(ClickableComponent))]
public class DamageByClickComponent : ComponentBase {
    [SerializeField]
    private int _damagePerClick = 1;

    protected override void Init(Action<string, Action<Object>> addAction, Action<string, Object> onSendAction) {
        base.Init(addAction, onSendAction);
        addAction.Invoke("Click", TakeDamage);
    }

    private void TakeDamage(Object data) {
        GetComponent<HpComponent>().TakeDamage(_damagePerClick);
    }
}