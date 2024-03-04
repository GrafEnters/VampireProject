using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour {
    [SerializeField]
    private Slider _hpSlider;

    [SerializeField]
    private TextMeshProUGUI _hpText;
    
    [SerializeField]
    private GameObject _interactableNotification;

    private static PlayerHud hud;
    private void Awake() {
        hud = this;
    }
    
    public static void SetHp(int amount, int max) {
        hud._hpSlider.value = (amount + 0f) / max;
        hud._hpText.text = $"{amount}/{max}";
    }
    
    public static void UpdateInteractableIndication(bool isActive) {
        hud._interactableNotification.SetActive(isActive);
    }
}