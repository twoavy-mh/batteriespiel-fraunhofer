using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class InfoController : MonoBehaviour
    {
        
        
        public GameObject prefabCanvas;
        
        private GameObject _infoCanvas;
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
            _infoCanvas = Instantiate(prefabCanvas);
            _infoCanvas.SetActive(true);
            _infoCanvas.GetComponentInChildren<CanvasGroup>().alpha = 0f;
            _infoCanvas.GetComponentInChildren<CanvasGroup>().DOFade(1, .5f).SetEase(Ease.InCubic);

            _closeButton = _infoCanvas.GetComponentInChildren<Button>();
            _closeButton.onClick.AddListener(CloseInfoModal);
        }
    
        private void CloseInfoModal()
        {
            _infoCanvas.GetComponentInChildren<CanvasGroup>().DOFade(0, .5f).SetEase(Ease.InCubic).OnComplete(() =>
            { 
                _closeButton.onClick.RemoveListener(CloseInfoModal);
                DestroyImmediate(_closeButton);
                
                _infoCanvas.SetActive(false);
                DestroyImmediate(_infoCanvas);

                _infoCanvas = null;
                _closeButton = null;
            });
        }
    }
}

