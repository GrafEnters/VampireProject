using UnityEngine;

public class DialogBase : MonoBehaviour {
    public bool isActive = false;

    public virtual void Show() {
        isActive = true;
        gameObject.SetActive(isActive);
    }

    public virtual void Hide() {
        isActive = false;
        gameObject.SetActive(isActive);
        UIFactory.ChangeCursorState(true);
    }

    public virtual void Set(DialogDataBase dialogDataBase) { }
}