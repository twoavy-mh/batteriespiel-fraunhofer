using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
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
            LocalizationSettings.SelectedLocaleChanged += LocalizationChanged;
            SetLocalizedString();
        }

        private void LocalizationChanged(Locale locale)
        {
            SetLocalizedString();
        }
        
        private void SetLocalizedString()
        {
            if (_translationKey.Empty())
            {
                return;
            }

            Utility.GetTranslatedText(_translationKey, s =>
            {
                try
                {
                    GetComponent<TMP_Text>().text = s;
                    LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>()); 
                } catch {}
            });
        }
        
        private void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= LocalizationChanged;
        }
        
    }
   
}