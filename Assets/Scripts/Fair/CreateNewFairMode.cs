using System;
using System.Collections;
using Helpers;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace Fair
{
    [RequireComponent(typeof(Button))]
    public class CreateNewFairMode : MonoBehaviour
    {

        private bool _isCreating = false;

        private SelfTranslatingText _fairModeBodyText;
        private GameObject _createFairMode;
        private GameObject _isInFairModeGameObject;
        private GameObject _inputWrapperGameObject;
        
        private GameObject _leaveButtonGameObject;
        private Button _joinLeaveButton;
        private Button _resetButton;
        
        private bool _lock = false;
        private bool _isInFairMode = false;
        
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => Api.Instance != null);
            _isInFairMode = PlayerPrefs.HasKey("fairCode");
            GetComponent<Button>().onClick.AddListener(CreateNewFair);
            
            _fairModeBodyText = GameObject.Find("FairModeBody").GetComponent<SelfTranslatingText>();
            _createFairMode = GameObject.Find("CreateFairMode");
            _isInFairModeGameObject = GameObject.Find("IsInFairMode");
            _joinLeaveButton = GameObject.Find("Join/LeaveButton").GetComponent<Button>();
            _resetButton = GameObject.Find("ResetButton").GetComponent<Button>();
            _resetButton.onClick.AddListener(BackToLoginButWithCode);
            _inputWrapperGameObject = GameObject.Find("inputWrapper");

            
            if (PlayerPrefs.HasKey("fairCode"))
            {
                IsInFairMode();
            }
            else
            {
                NotInFairMode();
            }
        }
        
        private void CreateNewFair()
        {
            if (_isCreating) return;
            
            _isCreating = true;
            StartCoroutine(Api.Instance.CreateFair(s =>
            {
                _isCreating = false;
                if (s != null)
                {
                    PlayerPrefs.SetInt("fairCode", s.tradeShowCode);
                    GameManager.Instance.fairChangedEvent.Invoke(PlayerPrefs.GetInt("fairCode"));
                    
                    IsInFairMode();
                }
            }));
        }

        private void BackToLoginButWithCode()
        {
            PlayerPrefs.DeleteKey("uuid");
            PlayerPrefs.DeleteKey("bearer");
            SceneManager.LoadScene("Onboarding");
        }

        private void IsInFairMode()
        { 
            _fairModeBodyText.translationKey = "fairMode_current_fair";
            _createFairMode.SetActive(false);
            _isInFairModeGameObject.SetActive(true);
            
            _joinLeaveButton.onClick.RemoveListener(JoinFairMode);
            _joinLeaveButton.onClick.AddListener(LeaveFairMode);
            _joinLeaveButton.GetComponentInChildren<SelfTranslatingText>().translationKey = "fair_mode_label_leave";
            _isInFairMode = true;
        }

        private void NotInFairMode()
        {
            _fairModeBodyText.translationKey = "fair_mode_create_body";
            _createFairMode.SetActive(true);
            _isInFairModeGameObject.SetActive(false);
            
            _joinLeaveButton.onClick.RemoveListener(LeaveFairMode);
            _joinLeaveButton.onClick.AddListener(JoinFairMode);
            _joinLeaveButton.GetComponentInChildren<SelfTranslatingText>().translationKey = "fair_mode_label_join";
            _isInFairMode = false;
        }
        
        private void JoinFairMode()
        {
            if (_lock) return;
            _lock = true;
            IsInFairMode();
            int fairCode = int.Parse(gameObject.GetComponentInChildren<TMP_InputField>().text);
            StartCoroutine(Api.Instance.FairExists(fairCode, (t) =>
            {
                Debug.Log(fairCode);
                _lock = false;
                (bool e, int c) = t;
                if (e)
                {
                    PlayerPrefs.DeleteKey("uuid");
                    PlayerPrefs.SetInt("fairCode", c);  
                }
                else
                {
                    Debug.Log("Fair does not exist");
                    NotInFairMode();
                    PlayerPrefs.DeleteKey("fairCode");
                }
            }));
        }
        
        private void LeaveFairMode()
        {
            if (_lock) return;
            _lock = true;
            StartCoroutine(Api.Instance.LeaveFairMode(details =>
            {
                _lock = false;
                GameState.Instance.currentGameState = details;
                PlayerPrefs.DeleteKey("fairCode");
                NotInFairMode();
            }));
        }
    }
}