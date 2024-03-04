using System;
using System.Collections.Generic;
using UnityEngine;

public class UIFactory : MonoBehaviour {
    [SerializeField]
    private Transform _dialogsContainer;

    [SerializeField]
    private DialogsList _dialogsList;
    
    

    private static UIFactory factory;

    private List<DialogBase> _shownDialogs = new List<DialogBase>();

    private void Awake() {
        factory = this;
    }

    public static void ChangeCursorState(bool isLocked) {
        Cursor.visible = !isLocked;
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public static bool IsInDialog() {
        return factory._shownDialogs.Count >0;
    }
    
    public static DialogType ActiveDialog() {
        if (factory._shownDialogs.Count == 0) {
            return DialogType.None;
        }

        return factory._shownDialogs[0].DialogType;
    }

    public static DialogBase GetPrefabByType(DialogType type) {
        DialogBase rb = factory._dialogsList.Dialogs.Find(b => b.DialogType == type);
        if (rb == null) {
            Debug.LogError($"Dialog not found in list, type: {type.ToString()}");
            return null;
        }

        return Instantiate(rb, factory._dialogsContainer);
    }

    public static void ShowDialog(DialogType type, DialogDataBase data = null) {
        var dialog = GetPrefabByType(type);
        dialog.Set(data);
        dialog.Show();
        factory._shownDialogs.Add(dialog);
        ChangeCursorState(false);
    }

    public static void TryHideActiveDialog() {
        if (ActiveDialog() != DialogType.None) {
            HideDialog(ActiveDialog());
        }
    }

    public static void HideDialog(DialogType type) {
        DialogBase dialog = factory._shownDialogs.Find(d => d.DialogType == type);
        if (dialog == null) {
            return;
        }

        dialog.Hide();
        factory._shownDialogs.Remove(dialog);
        Destroy(dialog.gameObject);
        ChangeCursorState(true);
    }
}
[Serializable]
public enum DialogType {
    None = -1,
    Inventory = 0,
    ChestInteraction = 1,
    FurnaceInteraction = 2,
    BuildingSelection = 3,
    DeathDialog = 4,
    SkillSelection = 5,
    SkillAltar = 6
}

public class DialogDataBase { }