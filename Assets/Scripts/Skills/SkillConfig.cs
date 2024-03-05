using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SkillConfig", fileName = "SkillConfig", order = 1)]
public class SkillConfig : ScriptableObject {
    public SkillType SkillType;

    public float Cooldown;
}

public enum SkillType {
    None = 0,
    BloodDash = 1,
    BloodPowerAttack = 2,
    BloodShield = 3,
    BloodSpear = 4,
    BloodExplosion = 5
}