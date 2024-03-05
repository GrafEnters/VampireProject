using UnityEngine;

public class BloodPowerAttackSkill : SkillBase {
    private static readonly int BloodPowerAttack = Animator.StringToHash("BloodPowerAttack");
    public BloodPowerAttackSkill(Player player, SkillConfig cnfg) : base(player, cnfg) { }

    protected override void Skill() {
        Animator.SetTrigger(BloodPowerAttack);
    }
}