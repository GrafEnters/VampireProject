using System;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(InventoryComponent))]
[RequireComponent(typeof(ClickableComponent))]
public class OpenInventoryByClickComponent : ComponentBase {
    public override void Init(Action<string, Action<Object>> addAction, Action<string,Object> onSendAction) {
        base.Init(addAction, onSendAction);
        addAction.Invoke("Click", OpenInventory);
    }

    private void OpenInventory(Object obj) {
        Player player = obj as Player;
        InventoryComponent invComponent = GetComponent<InventoryComponent>();
        UIFactory.ShowChestInteraction( player.GetInventory(),invComponent.Inventory);
    }
}