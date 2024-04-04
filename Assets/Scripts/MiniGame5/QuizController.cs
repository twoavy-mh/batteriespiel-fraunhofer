using System;
using System.Collections;
using System.Collections.Generic;
using Minigame5.Classes;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Minigame5
{
    [System.Serializable]
    public class QuizController : MonoBehaviour
    {
        public event Action TimerEndEvent;
        public event Action MaximumQuizTimeEndEvent;
        
        public TextMeshProUGUI questionCount;
        public SelfTranslatingText questionText;
        
        [SerializeField]
        public GameObject[] answersButtons;
        public int correctAnswerIndex;
        
        private QuizSlots _quizSlots;
        
        public float timerAfterAnswer = 2;
        private float _timerAfterAnswerStartedTime;
        private bool _timerAfterAnswerStarted = false;

        public RectTransform quizProgress;
        public float maximumQuizTime = 120;
        private float? _quizStartTime = null;
        
    // Start is called before the first frame update
        void Awake()
        {
            _quizSlots = GameObject.Find("Main").GetComponent<QuizSlots>();
        }
        
        public void StartQuiz()
        {
            _quizStartTime = Time.time;
            Debug.Log($"Start Quiz {_quizStartTime.Value}");
        }

        // Update is called once per frame
        void Update()
        {
            if (_timerAfterAnswerStarted)
            {
                if (_timerAfterAnswerStartedTime + timerAfterAnswer < Time.time)
                {
                    _timerAfterAnswerStarted = false;
                    _timerAfterAnswerStartedTime = 0;
                    _quizSlots.IncrementSlotIndex();
                    TimerEndEvent?.Invoke();
                }
            }

            if (_quizStartTime != null)
            {
                quizProgress.localScale = new Vector3((Time.time - _quizStartTime.Value) / maximumQuizTime, 1, 1);
            }
            
            if (_quizStartTime + maximumQuizTime < Time.time)
            {
                MaximumQuizTimeEndEvent?.Invoke();
            }
        }
        
        public void SetQuizSlot(QuizSlot quizSlot, string questionCountText)
        {
            questionCount.text = questionCountText; 
            questionText.translationKey = quizSlot.question;
            
            for (int i = 0; i < answersButtons.Length; i++)
            {
                answersButtons[i].GetComponentInChildren<SelfTranslatingText>().translationKey = quizSlot.answers[i];
                int answerIndex = i;
                answersButtons[i].GetComponent<Button>().onClick.AddListener(()=> { OnAnswerButtonClick(answerIndex); });
                answersButtons[i].GetComponent<QuizAnswerButtonController>().SetState(QuizAnswerButtonController.ButtonState.active);
            }
            
            correctAnswerIndex = quizSlot.correctAnswerIndex;
        }
        
        private void OnAnswerButtonClick(int answerIndex)
        {
            for (int i = 0; i < answersButtons.Length; i++)
            {
                answersButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            }
            
            SceneController.Instance.quizEvent.Invoke(answerIndex, correctAnswerIndex);
            _timerAfterAnswerStartedTime = Time.time;
            _timerAfterAnswerStarted = true;
        }
    }
}
