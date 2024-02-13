using System;
using Events;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame5
{
    public class QuizAnswerButtonController : MonoBehaviour, QuizEvent.IUseAnswered
    {
        private bool intialized = false; 
        
        public int buttonIndex;

        public GameObject imageCheck;
        public GameObject imageCross;
        
        private Button _button;
        private Image _borderColor;
        private TextMeshProUGUI _textColor;
        
        public enum ButtonState { active, inactive, wrong, correct }
        
        private void Start()
        {
            InitQuizAnswerButtonController();
        }
        private void Awake()
        {
            InitQuizAnswerButtonController();
        }

        private void OnDestroy()
        {
            Debug.Log("Destroy = " + gameObject.name);
        }

        private void InitQuizAnswerButtonController()
        {
            if (intialized) return;
            intialized = true;
            _button = GetComponent<Button>();
            _borderColor = GetComponent<Image>();
            _textColor = transform.GetComponentInChildren<TextMeshProUGUI>();
            SceneController.Instance.quizEvent.AddListener(UseAnswered);
        }

        public void SetState(ButtonState state)
        {
            switch (state)
            {
                case ButtonState.active: 
                    imageCheck.SetActive(false);
                    imageCross.SetActive(false);
                    _button.interactable = true;
                    _borderColor.color = Settings.ColorMap[Tailwind.Blue1];
                    _textColor.color = Settings.ColorMap[Tailwind.Blue1];
                    break;
                case ButtonState.inactive:
                    imageCheck.SetActive(false);
                    imageCross.SetActive(false);
                    _button.interactable = false;
                    _borderColor.color = Settings.ColorMap[Tailwind.Blue5];
                    _textColor.color = Settings.ColorMap[Tailwind.Blue5];
                    break;
                case ButtonState.correct:
                    imageCheck.SetActive(true);
                    _button.interactable = false;
                    _borderColor.color = Settings.ColorMap[Tailwind.Yellow3];
                    _textColor.color = Settings.ColorMap[Tailwind.Yellow3];
                    break;
                case ButtonState.wrong:
                    imageCross.SetActive(true);
                    _button.interactable = false;
                    _borderColor.color = Settings.ColorMap[Tailwind.Blue1];
                    _borderColor.color = Settings.ColorMap[Tailwind.Blue1];
                    break;
            }
        }

        public void UseAnswered(int answerIndex, int correctAnswerIndex)
        {
            if (buttonIndex == correctAnswerIndex)
            {
                SetState(ButtonState.correct);
            }
            else if (buttonIndex == answerIndex)
            {
                SetState(ButtonState.wrong);
            }
            else
            {
                SetState(ButtonState.inactive);
            }
        }
    }
}