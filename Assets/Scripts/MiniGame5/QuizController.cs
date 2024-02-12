using System.Collections;
using System.Collections.Generic;
using Minigame5.Classes;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame5
{
    [System.Serializable]
    public class QuizController : MonoBehaviour
    {
        
        public TextMeshProUGUI questionCount;
        public SelfTranslatingText questionText;
        
        [SerializeField]
        public GameObject[] answersButtons;
        public int correctAnswerIndex;
        
        private QuizSlots _quizSlots;
        
        public float TimerAfterAnswer = 2;
        private float TimerAfterAnswerStartedTime;
        private bool TimerAfterAnswerStarted = false;
        
    // Start is called before the first frame update
        void Awake()
        {
            _quizSlots = GameObject.Find("Main").GetComponent<QuizSlots>();
        }

        // Update is called once per frame
        void Update()
        {
            if (TimerAfterAnswerStarted)
            {
                if (TimerAfterAnswerStartedTime + TimerAfterAnswer < Time.time)
                {
                    TimerAfterAnswerStarted = false;
                    TimerAfterAnswerStartedTime = 0;
                    _quizSlots.IncrementSlotIndex();
                }
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
            SceneController.Instance.quizEvent.Invoke(answerIndex, correctAnswerIndex);
            TimerAfterAnswerStartedTime = Time.time;
            TimerAfterAnswerStarted = true;
        }
    }
}
