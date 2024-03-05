using System;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventsController : MonoBehaviour {
    [SerializeField]
    private UnityEvent _event1, _event2, _event3, _event4;

    private Action _event1Action, _event2Action, _event3Action, _event4Action;

    public void SetEventAction(Action action, int index) {
        switch (index) {
            case 1:
                _event1Action = action;
                break;
            case 2:
                _event2Action = action;
                break;
            case 3:
                _event3Action = action;
                break;
            case 4:
                _event4Action = action;
                break;
        }
    }

    public void TriggerEvent1() {
        _event1?.Invoke();
        _event1Action?.Invoke();
        _event1Action = null;
    }

    public void TriggerEvent2() {
        _event2?.Invoke();
        _event2Action?.Invoke();
        _event2Action = null;
    }

    public void TriggerEvent3() {
        _event3?.Invoke();
        _event3Action?.Invoke();
        _event3Action = null;
    }

    public void TriggerEvent4() {
        _event4?.Invoke();
        _event4Action?.Invoke();
        _event4Action = null;
    }
}