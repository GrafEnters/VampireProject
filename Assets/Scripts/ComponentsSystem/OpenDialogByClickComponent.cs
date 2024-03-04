using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class OpenDialogByActionComponent : ComponentBase {
    [SerializeField]
    private DialogType _dialogType;

    [SerializeField]
    private ComponentAction _actionName = ComponentAction.Interact;
    
    protected override void Init(Action<ComponentAction, Action<Object>> addAction, Action<ComponentAction, Object> onSendAction) {
        base.Init(addAction, onSendAction);
        addAction.Invoke(_actionName, OpenDialog);
    }

    private void OpenDialog(Object obj) {
        DialogDataBase data = (_container as ICreateDialogData).GetDialogData(obj);
        UIFactory.ShowDialog(_dialogType, data);
    }
}