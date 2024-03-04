using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogsList", fileName = "DialogsTable", order = 1)]
public class DialogsList : ScriptableObject {
    public List<DialogBase> Dialogs;
}