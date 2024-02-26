using UnityEngine;

public class HpComponent : ComponentBase {
    private int _currenthp;

    [SerializeField]
    private int _maxHp;

    [SerializeField]
    private bool _destroyOnDeath = true;

    private void Awake() {
        RefillHealth();
    }

    public void TakeDamage(int amount) {
        _currenthp -= amount;
        _sendAction?.Invoke("TakeDamage", null);
        if (_currenthp <= 0) {
            Die();
        }
    }

    private void Die() {
        _sendAction?.Invoke("Die", null);
        //TODO enable ragdoll here
        if (_destroyOnDeath) {
            Destroy(gameObject);
        }
    }

    public void RefillHealth() {
        _currenthp = _maxHp;
    }
}