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
        private Button _button;
        private Image _borderColor;
        private TextMeshProUGUI _textColor;
        
        public enum ButtonState { active, inactive, wrong, correct }
        
        // Start is called before the first frame update
        private void Start()
        {
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
                    _button.interactable = true;
                    _borderColor.color = Settings.ColorMap[Tailwind.Blue1];
                    _textColor.color = Settings.ColorMap[Tailwind.Blue1];
                    break;
                case ButtonState.inactive:
                    _button.interactable = false;
                    _borderColor.color = Settings.ColorMap[Tailwind.Blue5];
                    _textColor.color = Settings.ColorMap[Tailwind.Blue5];
                    break;
                case ButtonState.correct:
                    _button.interactable = false;
                    _borderColor.color = Settings.ColorMap[Tailwind.Yellow3];
                    _textColor.color = Settings.ColorMap[Tailwind.Yellow3];
                    break;
                case ButtonState.wrong:
                    _button.interactable = false;
                    _borderColor.color = Settings.ColorMap[Tailwind.Blue1];
                    _borderColor.color = Settings.ColorMap[Tailwind.Blue1];
                    break;
            }
        }

        public void UseAnswered(int answerIndex, int correctAnswerIndex)
        {
            Debug.Log(answerIndex);
            Debug.Log(correctAnswerIndex);
            Debug.Log("Answered");
        }
    }
}