using Events;
using Helpers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Minigame5
{
    public class SceneController : MonoBehaviour, QuizEvent.IUseAnswered
    {
        private static SceneController _instance;
        
        private float _time;
        private float _completedTime = 0;
        
        private QuizSlots _quizSlots;
        private int correctlyAnswered = 0;

        public QuizEvent quizEvent;
        
        public GameObject quizMasterDesktop; 
        public GameObject quizMasterMobile;
        private QuizController _quizController;
        
        public GameObject startModalDesktop;
        public GameObject startModalMobile;

        public GameObject videoCanvas;
        
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
                startModalMobile.SetActive(true);
                startModalMobile.GetComponentInChildren<Button>().onClick.AddListener(StartVideo);
            }
            else
            {
                startModalDesktop.SetActive(true);
                startModalDesktop.GetComponentInChildren<Button>().onClick.AddListener(StartVideo);
            }
        }

        private void StartVideo()
        {
            startModalDesktop.SetActive(false);
            startModalMobile.SetActive(false);
            videoCanvas.SetActive(true);
            foreach (Button buttonInChild in videoCanvas.GetComponentsInChildren<Button>())
            {
                if (buttonInChild.name == "SkipButton") { 
                    buttonInChild.onClick.AddListener(StartQuiz);
                }
            }
        }
        
        private void StartQuiz()
        {
            videoCanvas.SetActive(false);
            if (Utility.GetDevice() == Device.Mobile)
            {
                quizMasterMobile.SetActive(true);
                _quizController = quizMasterMobile.GetComponent<QuizController>();
            }
            else
            {
                quizMasterDesktop.SetActive(true);
                _quizController = quizMasterDesktop.GetComponent<QuizController>();
            }
            _quizController.TimerEndEvent += TimerEnded;
            _quizController.MaximumQuizTimeEndEvent += SetEndscreen;
            _quizSlots.InitQuiz();
            
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
                _completedTime = Time.time - _time;
            }
        }
        
        private void TimerEnded()
        {
            if (_completedTime != 0)
            {
                SetEndscreen();
            }
        }

        private void SetEndscreen()
        {
            _quizController.MaximumQuizTimeEndEvent -= SetEndscreen;
            _quizController.TimerEndEvent -= TimerEnded;
            
            if (_completedTime == 0) _completedTime = Time.time - _time;
            
            int answerCount = _quizSlots.GetSlotsListLengh();
            if (Utility.GetDevice() == Device.Desktop)
            {
                quizMasterDesktop.SetActive(false);
                finishedModalDesktop.SetActive(true);
                finishedModalDesktop.GetComponent<MicrogameFinished>().SetScore(correctlyAnswered, answerCount, _completedTime);
            }
            else
            {
                quizMasterMobile.SetActive(false);
                finishedModalMobile.SetActive(true);
                finishedModalMobile.GetComponent<MicrogameFinished>().SetScore(correctlyAnswered, answerCount, _completedTime);
            }
        }
    }   
}
