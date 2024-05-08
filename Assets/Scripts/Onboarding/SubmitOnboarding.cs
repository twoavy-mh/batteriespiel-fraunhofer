using System.Collections;
using System.Collections.Generic;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Onboarding
{
    public class SubmitOnboarding : MonoBehaviour
    {
    
        public TMP_InputField nameInput;
        public Button submitButton;
        private bool _isSubmitting = false;
    
        // Start is called before the first frame update
        void Start()
        {
            submitButton.onClick.AddListener(Submit);
        }

        private void Submit()
        {
            if (_isSubmitting) return;
            _isSubmitting = true;
            Api.Instance.GetPlayerDetails(nameInput.text, GameObject.Find("LanguageToggle").GetComponent<LanguageToggleController>().GetCurrentLanguage(),
                s =>
                {
                    if (s != null)
                    {
                        Api.Instance.ReserializeGamestate(s, details =>
                        {
                            SceneManager.LoadScene("MainMenu"); 
                        });
                    } 
                });
        }
    }
}
