using UnityEngine;
using UnityEngine.Events;

public class AnimationEventsController : MonoBehaviour {
    [SerializeField]
    private UnityEvent _event1, _event2, _event3, _event4;

    public void TriggerEvent1() {
        _event1.Invoke();
    }

    public void TriggerEvent2() {
        _event2.Invoke();
    }

    public void TriggerEvent3() {
        _event3.Invoke();
    }

    public void TriggerEvent4() {
        _event4.Invoke();
    }
}