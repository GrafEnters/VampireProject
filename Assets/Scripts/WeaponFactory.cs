using UnityEngine;

public class WeaponFactory : MonoBehaviour {
    [SerializeField]
    private Transform _weaponsContainer;

    [SerializeField]
    private WeaponsList _weapons;

    //TODO Add pooling

    public static WeaponConfig GetConfigByType(WeaponType type) {
        var factory = FindObjectOfType<WeaponFactory>();
        return factory._weapons.Weapons.Find(b => b.WeaponType == type);
    }
    
    public static WeaponBase GetPrefabByType(WeaponType type) {
        var factory = FindObjectOfType<WeaponFactory>();
        WeaponConfig cnfg = GetConfigByType(type);
        if (cnfg == null) {
            Debug.LogError($"Resource not found in list, type: {type.ToString()}");
            return null;
        }

        return Instantiate(cnfg.Prefab, factory._weaponsContainer);
    }
}