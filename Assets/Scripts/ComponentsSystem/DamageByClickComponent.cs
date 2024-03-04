using System;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(HpComponent))]
[RequireComponent(typeof(ClickableComponent))]
public class DamageByClickComponent : ComponentBase {
    [SerializeField]
    private bool _isEnabled = false;
    [SerializeField]
    private int _damagePerClick = 1;

    protected override void Init(Action<ComponentAction, Action<Object>> addAction, Action<ComponentAction, Object> onSendAction) {
        base.Init(addAction, onSendAction);
        addAction.Invoke(ComponentAction.Click, TakeDamage);
    }

    private void TakeDamage(Object data) {
        if (!_isEnabled) {
            return;
        }
        GetComponent<HpComponent>().TakeDamage(_damagePerClick);
    }
}