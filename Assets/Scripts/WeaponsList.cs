using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponsTable", fileName = "WeaponsTable", order = 4)]
public class WeaponsList : ScriptableObject {
    public List<WeaponConfig> Weapons;
}