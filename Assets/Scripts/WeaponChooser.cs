using UnityEngine;

public class WeaponChooser : MonoBehaviour {
    [SerializeField]
    private Transform _weaponContainer;

    private WeaponBase _currentWeapon;
    private HpComponent _player;

    public void Init(HpComponent player) {
        _player = player;
        ChangeWeapon(WeaponType.None);
    }

    public void ChangeWeapon(WeaponType type) {
        if (_currentWeapon != null) {
            Destroy(_currentWeapon.gameObject);
            _currentWeapon = null;
        }

        if (type == WeaponType.None) {
            return;
        }

        WeaponBase weapon = WeaponFactory.GetPrefabByType(type);
        weapon.transform.SetParent(_weaponContainer);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        _currentWeapon = weapon;
        _currentWeapon.Init(_player,WeaponFactory.GetConfigByType(type));
    }
    
    public void SetCollisionsDetection(bool isEnabled) {
        if (_currentWeapon == null) {
            return;
        }
        _currentWeapon.SetCollisionsDetection(isEnabled);
    }
}