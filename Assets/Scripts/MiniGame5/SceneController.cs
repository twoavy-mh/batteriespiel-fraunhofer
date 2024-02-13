using Events;
using Helpers;
using UnityEngine;

namespace Minigame5
{
    public class SceneController : MonoBehaviour, QuizEvent.IUseAnswered
    {
        private static SceneController _instance;
        
        private float _time;
        
        private QuizSlots _quizSlots;
        private int correctlyAnswered = 0;

        public QuizEvent quizEvent;
        
        public GameObject quizMasterDesktop; 
        public GameObject quizMasterMobile; 
        
        public GameObject finishedModalDesktop;
        public GameObject finishedModalMobile;
        
        public static SceneController Instance
        {
            get
            {
                if (_instance == null) Debug.Log("no SceneController yet");
                return _instance;
            }
        }

        private void Awake()
        {
            InitSceneController();
        }
        
        private void InitSceneController()
        {
            _quizSlots = GetComponent<QuizSlots>();
            
            quizEvent ??= new QuizEvent();
            quizEvent.AddListener(UseAnswered);
            
            _instance = this;

            if (Utility.GetDevice() == Device.Mobile)
            {
                quizMasterMobile.SetActive(true);
            }
            else
            {
                quizMasterDesktop.SetActive(true);
            }

            _time = Time.time;
        }

        public void UseAnswered(int answerIndex, int correctAnswerIndex)
        {
            int index = _quizSlots.GetCurrentSlotIndex();
            int answerCount = _quizSlots.GetSlotsListLengh();
            if (correctAnswerIndex == answerIndex)
            {
                correctlyAnswered++;
            }
            Debug.Log("Correctly Answered" + correctlyAnswered + " | index =" + index);
            if (index == answerCount - 1)
            {
                float completedTime = Time.time - _time;
                if (Utility.GetDevice() == Device.Desktop)
                {
                    quizMasterDesktop.SetActive(false);
                    finishedModalDesktop.SetActive(true);
                    finishedModalDesktop.GetComponent<MicrogameFinished>().SetScore(correctlyAnswered, answerCount, completedTime);
                }
                else
                {
                    quizMasterMobile.SetActive(false);
                    finishedModalMobile.SetActive(true);
                    finishedModalMobile.GetComponent<MicrogameFinished>().SetScore(correctlyAnswered, answerCount, completedTime);
                }
            }
            
        }

        public void Die()
        {
            
        }
        
    }   
}
