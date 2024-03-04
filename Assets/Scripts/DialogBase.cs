using UnityEngine;

public class DialogBase : MonoBehaviour {

    [SerializeField]
    private DialogType _dialogType;

    public DialogType DialogType => _dialogType;
    
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
    
    public void HideButton() {
        UIFactory.HideDialog(DialogType);
    }
}