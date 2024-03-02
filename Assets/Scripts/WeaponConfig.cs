using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponConfig", fileName = "WeaponConfig", order = 1)]
public class WeaponConfig : ScriptableObject {
    public WeaponType WeaponType;
    public int Damage;
    public WeaponBase Prefab;
}