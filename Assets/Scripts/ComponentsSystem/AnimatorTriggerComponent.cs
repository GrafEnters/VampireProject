using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class AnimatorTriggerComponent : ComponentBase {
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private List<AnimTriggerMap> _animTriggers;

    protected override void Init(Action<string, Action<Object>> addAction, Action<string, Object> onSendAction) {
        base.Init(addAction, onSendAction);
        foreach (var VARIABLE in _animTriggers) {
            addAction.Invoke(VARIABLE.ComponentEvent, delegate { _animator.SetTrigger(VARIABLE.AnimationTrigger); });
        }
    }
}

[Serializable]
public class AnimTriggerMap {
    public string ComponentEvent;
    public string AnimationTrigger;
}