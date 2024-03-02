using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Player : CreatureBase {

    [SerializeField]
    private WeaponChooser _weaponChooser;
    
    public static Player CurrentPlayer;
    private static readonly int Attack = Animator.StringToHash("Attack");

    public ConstructingManager ConstructingManager => GetComponent<ConstructingManager>();

    public Inventory GetInventory() => GetComponent<InventoryComponent>().Inventory;

    protected override void Awake() {
        base.Awake();
        CurrentPlayer = this;
        UIFactory.ChangeCursorState(true);
        AddAction("Die", ShowDeathDialog);
        _weaponChooser.Init(GetComponent<HpComponent>());
    }

    private void Update() {
        if (UIFactory.IsInDialog()) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (InventoryDialog.StaticActive) {
                UIFactory.HideDialog(DialogType.Inventory);
            } else {
                InventoryDialogData data = new InventoryDialogData {
                    Player = GetInventory()
                };

                UIFactory.ShowDialog(DialogType.Inventory, data);
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            if (BuildingSelectionDialog.StaticActive) {
                UIFactory.HideDialog(DialogType.BuildingSelection);
            } else {
                BuildingSelectionDialogData data = new BuildingSelectionDialogData {
                    Player = this
                };
                UIFactory.ShowDialog(DialogType.BuildingSelection, data);
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            _animator.SetTrigger(Attack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _weaponChooser.ChangeWeapon(WeaponType.None);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            _weaponChooser.ChangeWeapon(WeaponType.Sword);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            _weaponChooser.ChangeWeapon(WeaponType.Axe);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            _weaponChooser.ChangeWeapon(WeaponType.Hammer);
        }
    }

    private void ShowDeathDialog(Object data) {
        DeathDialogData dialogData = new DeathDialogData {
            Player = this
        };
        UIFactory.ShowDialog(DialogType.DeathDialog, dialogData);
    }

    //Execute from animation
    public void SetWeaponCollisionDetection(bool isEnabled) {
        _weaponChooser.SetCollisionsDetection(isEnabled);
    }
}

