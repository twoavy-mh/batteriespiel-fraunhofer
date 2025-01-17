using System;
using Helpers;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace JumpNRun
{
    public class MaskotAndBadge : MonoBehaviour
    {
        [Serializable]
        public struct MaskotAndBadgeStruct
        {
            public Sprite maskot;
            public Sprite badge;
            public Sprite badgeEn;
        }
        
        [SerializeField] public MaskotAndBadgeStruct[] maskotAndBadgeStructs;

        private void Awake()
        {
            int i = (int)GameState.Instance.GetCurrentMicrogame();
            
            if (i == 6)
            {
                transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                return;
            };
            
            if (maskotAndBadgeStructs[i].maskot)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = maskotAndBadgeStructs[i].maskot;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            }
            
            Sprite posBadge = LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("de") ? maskotAndBadgeStructs[i].badge : maskotAndBadgeStructs[i].badgeEn;
            if (posBadge)
            {
                transform.GetChild(1).GetComponent<Image>().sprite = posBadge;
                transform.GetChild(1).GetComponent<Image>().SetNativeSize();
            }
            else
            {
                transform.GetChild(1).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
}