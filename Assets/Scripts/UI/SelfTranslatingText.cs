using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelfTranslatingText : MonoBehaviour
    {
        [SerializeField]
        private string _translationKey = "";
        public string translationKey
        {
            get {
                return _translationKey;
            } set {
                _translationKey = value;
                SetLocalizedString();
            }
        }

        void Start()
        {
            SetLocalizedString();
        }

        private void SetLocalizedString()
        {
            if (_translationKey.Empty())
            {
                return;
            }

            Utility.GetTranslatedText(_translationKey, s => GetComponent<TMP_Text>().text = s);
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
        }
    }
   
}