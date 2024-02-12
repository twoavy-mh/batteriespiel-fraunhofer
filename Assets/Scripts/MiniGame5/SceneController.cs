using Events;
using Helpers;
using UnityEngine;

namespace Minigame5
{
    public class SceneController : MonoBehaviour, QuizEvent.IUseAnswered
    {
        private static SceneController _instance;
        
        private int correctlyAnswered = 0;

        public QuizEvent quizEvent;
        
        public GameObject quizMasterDesktop; 
        public GameObject quizMasterMobile; 
        
        public GameObject finishedModal;
        
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
            quizEvent ??= new QuizEvent();
            
            _instance = this;

            if (Utility.GetDevice() == Device.Desktop)
            {
                quizMasterMobile.SetActive(false);
            }
            else
            {
                quizMasterDesktop.SetActive(false);
            }
            
        }

        public void UseAnswered(int answerIndex, int correctAnswerIndex)
        {
            if (correctAnswerIndex == answerIndex)
            {
                correctlyAnswered++;
            }
            
            if (answerIndex == 5)
            {
                finishedModal.GetComponent<MicrogameFinished>().SetScore(correctlyAnswered);
            }
            
        }

        public void Die()
        {
            
        }
        
    }   
}
