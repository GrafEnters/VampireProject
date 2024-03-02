using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour {
    private List<HpComponent> _ignoredHpComponents = new List<HpComponent>();
    private const float ATTACK_DELAY = 0.5f;
    private WeaponConfig _config;

    private Rigidbody _rb;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(HpComponent _player, WeaponConfig config) {
        _ignoredHpComponents.Add(_player);
        _config = config;
    }

    public void SetCollisionsDetection(bool isEnabled) {
        _rb.detectCollisions = isEnabled;
    }
    

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody != null) {
            var hpComponent = other.attachedRigidbody.GetComponent<HpComponent>();
            if (!hpComponent) {
                return;
            }

            //Check to prevent doubleAttacking in one swing
            if (_ignoredHpComponents.Contains(hpComponent)) {
                return;
            }

            hpComponent.TakeDamage(_config.Damage);

            StartCoroutine(RememberHpComponent(hpComponent, ATTACK_DELAY));
        }
    }

    private IEnumerator RememberHpComponent(HpComponent hpComponent, float delay) {
        _ignoredHpComponents.Add(hpComponent);
        yield return new WaitForSeconds(delay);
        if (_ignoredHpComponents.Contains(hpComponent)) {
            _ignoredHpComponents.Remove(hpComponent);
        }
    }
}

public enum WeaponType {
    None = 0,
    Sword = 1,
    Axe = 2,
    Hammer = 3,
}