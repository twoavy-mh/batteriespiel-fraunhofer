using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;


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
            GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString(translationKey);
        }
    }
   
}