using System.Collections;
using UnityEngine;

public class SkillsChecker {
    private GameObject _seenObject;

    private SkillBase _skill1, _skill2, _skill3, _ultimateSkill;
    private SkillsList _skillsList;
    private Player _player;

    public void Init(Player player, SkillsList skillsList) {
        _skillsList = skillsList;
        _player = player;
        _skill1 = new BloodPowerAttackSkill(player,GetSkillByType(SkillType.BloodPowerAttack));
        //_skill1 = new BloodDashSkill(player,GetSkillByType(SkillType.BloodDash));
        _skill2 = new BloodShieldSkill(player,GetSkillByType(SkillType.BloodShield));
        _skill3 = new BloodSpearSkill(player,GetSkillByType(SkillType.BloodSpear));
        _ultimateSkill = new BloodExplosionSkill(player,GetSkillByType(SkillType.BloodExplosion));
    }

    private SkillConfig GetSkillByType(SkillType type) => _skillsList.Skills.Find(s => s.SkillType == type);

    public void CheckSkillsInputs() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            TryActivateSkill(_skill1);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            TryActivateSkill(_skill2);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            TryActivateSkill(_skill3);
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            TryActivateSkill(_ultimateSkill);
        }
    }

    private void TryActivateSkill(SkillBase skill) {
        if (skill.Cooldown != 0) {
            return;
        }

        if (skill.IsUsingSkillAnimation) {
            return;
        }

        skill.TryActivate(OnSkillActivated);
    }

    private void OnSkillActivated(SkillBase skill) {
        _player.StartCoroutine(WaitForSkillCooldown(skill));
    }

    private IEnumerator WaitForSkillCooldown(SkillBase skill) {
        skill.UpdateCooldown(skill.Config.Cooldown);
        while (skill.Cooldown > 0) {
            float newVal = skill.Cooldown - Time.deltaTime;
            if (newVal < 0) {
                newVal = 0;
            }

            skill.UpdateCooldown(newVal);
            yield return new WaitForEndOfFrame();
        }
    }
}