using System;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame2
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
            if (Utility.GetDevice() == Device.Mobile)
            {
                RectTransform rt = GameObject.Find("ScrollableCanvasContainer").GetComponent<RectTransform>();
                rt.localPosition = new Vector3(rt.localPosition.x, 0, 0);
            }
            gameObject.SetActive(false);
        }

        public void SetText(string header, string text)
        {
            if (Utility.GetDevice() == Device.Desktop)
            {
                Utility.GetTranslatedText(text, s => transform.GetChild(3).GetComponent<TMP_Text>().text = s);    
            }
            else
            {
                Utility.GetTranslatedText(text, s => GameObject.Find("InfoText").GetComponent<TMP_Text>().text = s);
            }
            Utility.GetTranslatedText(header, s => transform.GetChild(2).GetComponent<TMP_Text>().text = s.Replace("\n", ""));
            
        }
    }
   
}