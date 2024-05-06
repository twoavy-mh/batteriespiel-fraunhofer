using System.Collections;
using System.Collections.Generic;
using Extensions;
using Helpers;
using TMPro;
using UI;
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
            GameObject.Find("GoDown").GetComponent<Timebar>().Play();
            if (Utility.GetDevice() == Device.Mobile)
            {
                RectTransform rt = GameObject.Find("ScrollableCanvasContainer").GetComponent<RectTransform>();
                rt.localPosition = new Vector3(rt.localPosition.x, 0, 0);
            }
            gameObject.SetActive(false);
            if (SceneController.Instance.finishedCount == 6)
            {
                GameObject.Find("GoDown").GetComponent<Timebar>().Finished();
                GameObject[] modals = SceneController.Instance.GetBased();
                modals[0].GetComponent<MicrogameFourFinished>().SetScore(
                    SceneController.Instance.fails, 
                    SceneController.Instance.fails + SceneController.Instance.finishedCount, SceneController.Instance.sec);
            }
        }

        public void SetContent(string header, string text, Sprite sprite, bool isFinal = false)
        {
            /*if (isFinal)
            {
            //TODO: Mit GetComponentInChildren<SelfTranslatingText>
                transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<SelfTranslatingText>()
                    .translationKey = "end_game";
            }*/
            if (Utility.GetDevice() == Device.Desktop)
            {
                Utility.GetTranslatedText(text, s => transform.GetChild(3).GetComponent<TMP_Text>().text = s);
            }
            else
            {
                Utility.GetTranslatedText(text, s => GameObject.Find("InfoText").GetComponent<TMP_Text>().text = s);
            }
            Utility.GetTranslatedText(header, s => transform.GetChild(2).GetComponent<TMP_Text>().text = s);
            transform.GetChild(4).GetComponent<Image>().sprite = sprite;
            transform.GetChild(4).GetComponent<ImageBasedAspectRatioFitter>().UpdateRatioAndImage();
        }
    }    
}

