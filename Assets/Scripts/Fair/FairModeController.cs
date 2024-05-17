using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helpers;
using TMPro;
using UnityEngine.SceneManagement;

namespace UI
{
    public class FairModeController : MonoBehaviour
    {
        public GameObject prefabCanvas;
        
        private GameObject _fairModeCanvas;
        private SelfTranslatingText _fairModeBody;
        
        private Button _joinLeaveButton;
        private Button _closeButton;
        
        private GameObject _inputWrapperGameObject;
        private GameObject _fairModeCurrentFairGameObject;
        private GameObject _fairCodeGameObject;
        
        private bool _isInFairMode = false;
        private bool _lock = false;
        
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
            
            
            if (PlayerPrefs.HasKey("fairCode"))
            {
                _isInFairMode = true;
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
            _inputWrapperGameObject = GameObject.Find("inputWrapper");
            _fairCodeGameObject = GameObject.Find("fairCode");
            _fairModeCurrentFairGameObject = GameObject.Find("FairModeCurrentFair");
            
            Debug.Log(_isInFairMode);
            if (_isInFairMode)
            {
                JoinActions();
            }
            else
            {
                LeaveActions();
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
            if (_lock) return;
            _lock = true;
            int fairCode = int.Parse(_fairModeCanvas.GetComponentInChildren<TMP_InputField>().text);
            JoinActions();
            StartCoroutine(Api.Instance.FairExists(fairCode, (t) =>
            {
                Debug.Log(fairCode);
                _lock = false;
                (bool e, int c) = t;
                if (e)
                {
                    PlayerPrefs.DeleteKey("uuid");
                    PlayerPrefs.SetInt("fairCode", c);
                    SceneManager.LoadScene("Onboarding");    
                }
                else
                {
                    Debug.Log("Fair does not exist");
                    LeaveActions();
                    PlayerPrefs.DeleteKey("fairCode");
                }
            }));
        }
        
        private void LeaveFairMode()
        {
            if (_lock) return;
            _lock = true;
            LeaveActions();
            StartCoroutine(Api.Instance.LeaveFairMode(details =>
            {
                _lock = false;
                GameState.Instance.currentGameState = details;
                PlayerPrefs.DeleteKey("fairCode");
            }));
            Destroy(_fairModeCanvas);
        }
        
        private void JoinActions()  {
            _isInFairMode = true;
            _joinLeaveButton.onClick.RemoveListener(JoinFairMode);
            _joinLeaveButton.onClick.AddListener(LeaveFairMode);
            _joinLeaveButton.GetComponentInChildren<SelfTranslatingText>().translationKey = "fair_mode_label_leave";
            _fairModeBody.translationKey = "fair_mode_leave_body";
            
            _inputWrapperGameObject.SetActive(false);
            _fairCodeGameObject.SetActive(true);
            _fairModeCurrentFairGameObject.SetActive(true);
        }
        
        private void LeaveActions()  {
            _isInFairMode = false;
            _joinLeaveButton.onClick.RemoveListener(LeaveFairMode);
            _joinLeaveButton.onClick.AddListener(JoinFairMode);
            _joinLeaveButton.GetComponentInChildren<SelfTranslatingText>().translationKey = "fair_mode_label_join";
            _fairModeBody.translationKey = "fair_mode_join_body";
            
            _inputWrapperGameObject.SetActive(true);
            _fairCodeGameObject.SetActive(false);
            _fairModeCurrentFairGameObject.SetActive(false);
        }
        
    }
}

