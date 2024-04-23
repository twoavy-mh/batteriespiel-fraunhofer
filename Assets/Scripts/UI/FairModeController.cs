using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helpers;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class FairModeController : MonoBehaviour
    {
        public GameObject prefabCanvas;
        
        private GameObject _fairModeCanvas;
        private SelfTranslatingText _fairModeBody;
        private Button _joinLeaveButton;
        private Button _closeButton;
        
        private bool _isInFairMode = false;
        
        void OnEnable()
        {
            if (GameState.Instance.currentGameState == null)
            {
                _isInFairMode = false;
            }
            else
            {
                _isInFairMode = GameState.Instance.currentGameState.tradeShowCode > 0;   
            }
             
            transform.GetComponent<Button>().onClick.AddListener(OpenInfoModal);
        }
        
        void OnDisable()
        {
            transform.GetComponent<Button>().onClick.RemoveListener(OpenInfoModal);
        }

        private void OpenInfoModal()
        {
            _fairModeCanvas = Instantiate(prefabCanvas);
            _fairModeCanvas.SetActive(true);
            _fairModeCanvas.GetComponentInChildren<CanvasGroup>().alpha = 0f;
            _fairModeCanvas.GetComponentInChildren<CanvasGroup>().DOFade(1, .5f).SetEase(Ease.InCubic);

            _fairModeBody = GameObject.Find("FairModeBody").GetComponent<SelfTranslatingText>();
            _joinLeaveButton = GameObject.Find("Join/LeaveButton"). GetComponent<Button>();
            Debug.Log(_isInFairMode);
            if (_isInFairMode)
            {
                _joinLeaveButton.GetComponentInChildren<SelfTranslatingText>().translationKey = "fair_mode_label_leave";
                _fairModeBody.translationKey = "fair_mode_leave_body";
                _joinLeaveButton.onClick.AddListener(LeaveFairMode);
            }
            else
            {
                _joinLeaveButton.GetComponentInChildren<SelfTranslatingText>().translationKey = "fair_mode_label_join";
                _fairModeBody.translationKey = "fair_mode_join_body";
                _joinLeaveButton.onClick.AddListener(JoinFairMode);
            }

            _closeButton = GameObject.Find("FairModeCloseButton"). GetComponent<Button>();
            _closeButton.onClick.AddListener(CloseInfoModal);
        }
    
        private void CloseInfoModal()
        {
            _fairModeCanvas.GetComponentInChildren<CanvasGroup>().DOFade(0, .5f).SetEase(Ease.InCubic).OnComplete(() =>
            { 
                _closeButton.onClick.RemoveListener(CloseInfoModal);
                DestroyImmediate(_closeButton);
                
                _fairModeCanvas.SetActive(false);
                DestroyImmediate(_fairModeCanvas);

                _fairModeCanvas = null;
                _closeButton = null;
            });
        }
        
        private void JoinFairMode()
        {
            _isInFairMode = true;
            _joinLeaveButton.onClick.RemoveListener(JoinFairMode);
            _joinLeaveButton.onClick.AddListener(LeaveFairMode);
            _joinLeaveButton.GetComponentInChildren<SelfTranslatingText>().translationKey = "fair_mode_label_leave";
            _fairModeBody.translationKey = "fair_mode_leave_body";
            int fairCode = int.Parse(_fairModeCanvas.GetComponentInChildren<TMP_InputField>().text);
            PlayerPrefs.DeleteKey("uuid");
            PlayerPrefs.SetInt("fairCode", fairCode);
            SceneManager.LoadScene("Onboarding");
        }
        
        private void LeaveFairMode()
        {
            _isInFairMode = false;
            _joinLeaveButton.onClick.RemoveListener(LeaveFairMode);
            _joinLeaveButton.onClick.AddListener(JoinFairMode);
            _joinLeaveButton.GetComponentInChildren<SelfTranslatingText>().translationKey = "fair_mode_label_join";
            _fairModeBody.translationKey = "fair_mode_join_body";
            StartCoroutine(Api.Instance.LeaveFairMode(details =>
            {
                GameState.Instance.currentGameState = details;
                PlayerPrefs.DeleteKey("fairCode");
            }));
            Destroy(_fairModeCanvas);
        }
    }
}

