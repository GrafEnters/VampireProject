using TMPro;
using UnityEngine;

namespace DefaultNamespace {
    public class InventoryItemView : MonoBehaviour {
        [SerializeField]
        private TMP_Text _nameText, _amountText;

        
        public void Clear() {
            _nameText.text = "";
            _amountText.text = "";
        }
        public void Set(string resourceName, string amount) {
            _nameText.text = resourceName;
            _amountText.text = amount.ToString();
        }
    }
}