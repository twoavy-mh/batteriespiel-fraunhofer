using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Minigame4
{
    public class SelfTranslatingTest : MonoBehaviour
    {

        public string translationKey;
    
        void Start()
        {
            GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString(translationKey);
        }
    }
   
}