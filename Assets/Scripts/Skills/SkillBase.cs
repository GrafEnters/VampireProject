using System;
using UnityEngine;

public abstract class SkillBase {
    private SkillConfig _cnfg;
    private Player _player;
    private float _cooldown;
    private static readonly int UsingSkill = Animator.StringToHash("UsingSkill");

    public SkillConfig Config => _cnfg;

    public bool IsUsingSkillAnimation => Animator.GetBool(UsingSkill) || _waitActivation;

    private bool _waitActivation = false;
    protected Animator Animator => _player.Animator;
    public float Cooldown => _cooldown;

    public SkillBase(Player player, SkillConfig cnfg) {
        _cnfg = cnfg;
        _player = player;
    }

    public void UpdateCooldown(float value) {
        _cooldown = value;
    }

    public void TryActivate(Action<SkillBase> onActivated) {
        _waitActivation = true;
        var animController = Animator.gameObject.GetComponent<AnimationEventsController>();
        Skill();
        animController.SetEventAction(delegate {
            onActivated?.Invoke(this);
            _waitActivation = false;
        }, 1);
    }

    protected abstract void Skill();
}