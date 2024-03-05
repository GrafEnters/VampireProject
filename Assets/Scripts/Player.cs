using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Player : CreatureBase {
    [SerializeField]
    private WeaponChooser _weaponChooser;

    [SerializeField]
    private SkillsList _skillsList;

    public static Player CurrentPlayer;
    private static readonly int Attack = Animator.StringToHash("Attack");
    private InteractionChecker _interactionChecker = new InteractionChecker();
    private SkillsChecker _skillsChecker = new SkillsChecker();
    public ConstructingManager ConstructingManager => GetComponent<ConstructingManager>();

    public Animator Animator => _animator;

    public Inventory GetInventory() => GetComponent<InventoryComponent>().Inventory;

    protected override void Awake() {
        base.Awake();
        CurrentPlayer = this;
        UIFactory.ChangeCursorState(true);
        AddAction(ComponentAction.Die, ShowDeathDialog);
        AddAction(ComponentAction.TakeDamage, UpdateHp);
        HpComponent hpComponent = GetComponent<HpComponent>();
        _weaponChooser.Init(hpComponent);
        _skillsChecker.Init(this, _skillsList);
    }

    private void Start() {
        HpComponent hpComponent = GetComponent<HpComponent>();
        UpdateHp(hpComponent);
    }

    private void Update() {
        _interactionChecker.UpdateSeenObject();

        CheckDialogsInputs();

        if (UIFactory.IsInDialog()) {
            return;
        }

        CheckWorldInputs();
        _skillsChecker.CheckSkillsInputs();
    }

    private void CheckDialogsInputs() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (UIFactory.ActiveDialog() == DialogType.Inventory) {
                UIFactory.HideDialog(DialogType.Inventory);
            } else if (UIFactory.ActiveDialog() == DialogType.None) {
                InventoryDialogData data = new InventoryDialogData {
                    Player = GetInventory()
                };

                UIFactory.ShowDialog(DialogType.Inventory, data);
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            if (UIFactory.ActiveDialog() == DialogType.BuildingSelection) {
                UIFactory.HideDialog(DialogType.BuildingSelection);
            } else if (UIFactory.ActiveDialog() == DialogType.None) {
                BuildingSelectionDialogData data = new BuildingSelectionDialogData {
                    Player = this
                };
                UIFactory.ShowDialog(DialogType.BuildingSelection, data);
            }
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            if (UIFactory.ActiveDialog() == DialogType.SkillSelection) {
                UIFactory.HideDialog(DialogType.BuildingSelection);
            } else if (UIFactory.ActiveDialog() == DialogType.None) {
                SkillSelectionDialogData data = new SkillSelectionDialogData() { };
                UIFactory.ShowDialog(DialogType.SkillSelection, data);
            }
        }

        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            UIFactory.TryHideActiveDialog();
        }
    }

    private void UpdateHp(Object data) {
        var hpComponent = data as HpComponent;
        PlayerHud.SetHp(hpComponent.Hp, hpComponent.MaxHp);
    }

    private void ShowDeathDialog(Object data) {
        DeathDialogData dialogData = new DeathDialogData {
            Player = this
        };
        UIFactory.TryHideActiveDialog();
        UIFactory.ShowDialog(DialogType.DeathDialog, dialogData);
    }

    private void CheckWorldInputs() {
        if (Input.GetKeyDown(KeyCode.F)) {
            InteractableComponent interactable = _interactionChecker.CheckInteractableObject();
            if (interactable != null) {
                interactable.Interact();
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

    //Execute from animation
    public void SetWeaponCollisionDetection(bool isEnabled) {
        _weaponChooser.SetCollisionsDetection(isEnabled);
    }
}