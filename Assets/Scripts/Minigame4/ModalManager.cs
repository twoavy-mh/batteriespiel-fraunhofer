using System.Collections;
using System.Collections.Generic;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;


namespace Minigame4
{
    public class ModalManager : MonoBehaviour
    {
    
        private Button _closeModalButton;
    
        // Start is called before the first frame update
        void Start()
        {
            _closeModalButton = transform.GetChild(1).GetComponent<Button>();
            _closeModalButton.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            gameObject.SetActive(false);
        }

        public void SetText(string header, string text)
        {
            Utility.GetTranslatedText(header, s => transform.GetChild(3).GetComponent<TMP_Text>().text = s);
            Utility.GetTranslatedText(text, s => transform.GetChild(2).GetComponent<TMP_Text>().text = s);
        }
    }    
}

