using UnityEngine;

public class WolfCC : CreatureBase {
    [SerializeField]
    private FieldOfView _fov;

    protected override void Awake() {
        _fov.Init(IsImportant, null, null);
    }

    private bool IsImportant(GameObject obj) {
        if (obj == gameObject) {
            return false;
        }

        if (obj.GetComponent<CreatureBase>()) {
            return true;
        }

        return false;
    }

}