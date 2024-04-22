using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SettingsController : MonoBehaviour
    {
        public GameObject prefabCanvas;
        
        private GameObject _settingsCanvas;
        
        private Button _resetPlayerPrefsButton;
        private Button _closeButton;
        
        void OnEnable()
        {
            transform.GetComponent<Button>().onClick.AddListener(OpenInfoModal);
        }
        
        void OnDisable()
        {
            transform.GetComponent<Button>().onClick.RemoveListener(OpenInfoModal);
        }

        private void OpenInfoModal()
        {
            _settingsCanvas = Instantiate(prefabCanvas);
            _settingsCanvas.SetActive(true);
            _settingsCanvas.GetComponentInChildren<CanvasGroup>().alpha = 0f;
            _settingsCanvas.GetComponentInChildren<CanvasGroup>().DOFade(1, .5f).SetEase(Ease.InCubic);
            
            /*_resetPlayerPrefsButton = GameObject.Find("ResetPlayerPrefsButton")?.GetComponentInChildren<Button>();
            if (PlayerPrefs.HasKey("uuid"))
            {
                _resetPlayerPrefsButton?.onClick.AddListener(ResetPlayerPrefsModal);
            }
            else
            {
                _resetPlayerPrefsButton.gameObject.SetActive(false);
            }*/

            _closeButton = GameObject.Find("SettingsCloseButton").GetComponentInChildren<Button>();
            _closeButton.onClick.AddListener(CloseInfoModal);
        }
        
        /*private void ResetPlayerPrefsModal()
        {
            //TODO: Add a confirmation modal
            PlayerPrefs.DeleteKey("uuid");
            SceneManager.LoadScene("Onboarding");
        }*/
    
        private void CloseInfoModal()
        {
            _settingsCanvas.GetComponentInChildren<CanvasGroup>().DOFade(0, .5f).SetEase(Ease.InCubic).OnComplete(() =>
            { 
                _closeButton.onClick.RemoveListener(CloseInfoModal);
                DestroyImmediate(_closeButton);
                
                _settingsCanvas.SetActive(false);
                DestroyImmediate(_settingsCanvas);

                _settingsCanvas = null;
                _closeButton = null;
            });
        }
    }
}

