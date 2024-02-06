using System;
using UnityEngine;

namespace DefaultNamespace {
    public class HpComponent : ComponentBase {
        private int _currenthp;

        [SerializeField]
        private int _maxHp;

        private void Awake() {
            _currenthp = _maxHp;
        }

        public void TakeDamage(int amount) {
            _currenthp -= amount;
            _sendAction?.Invoke("TakeDamage");
            if (_currenthp <= 0) {
                Die();
            }
        }

        private void Die() {
            _sendAction?.Invoke("Die");
            Destroy(gameObject);
        }
    }
}