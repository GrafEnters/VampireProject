using System;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(ClickableComponent))]
public class OpenDialogByClickComponent : ComponentBase {
    [SerializeField]
    private DialogType _dialogType;

    protected override void Init(Action<string, Action<Object>> addAction, Action<string, Object> onSendAction) {
        base.Init(addAction, onSendAction);
        addAction.Invoke("Click", OpenDialog);
    }

    private void OpenDialog(Object obj) {
        DialogDataBase data = (_container as ICreateDialogData).GetDialogData(obj);
        UIFactory.ShowDialog(_dialogType, data);
    }
}