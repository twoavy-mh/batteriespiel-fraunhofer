using System;
using Events;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame3
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;

        public int startGame = 5;
        private int _nanoGameIndex;

        public float timerToNextScene = 2f;
        private float toNextSceneStartedTime;
        private bool toNextSceneStarted = false;
        
        private int correctlyAnswered = 0;

        public GameObject nanoGame1; 
        public GameObject nanoGame2; 
        public GameObject nanoGame3; 
        public GameObject nanoGame4; 
        public GameObject nanoGame5; 
        public GameObject nanoGame6; 
        public GameObject nanoGame7; 
        
        public GameObject startModalDesktop;
        public GameObject startModalMobile;
        
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
            _nanoGameIndex = startGame;

        }

        private void Update()
        {
            if (toNextSceneStarted)
            {
                if (toNextSceneStartedTime + timerToNextScene < Time.time)
                {
                    toNextSceneStarted = false;
                    if (startGame <= 7)
                    {
                        SetEndscreen();
                    }
                    else
                    {
                        startGame++;
                        SetNextNanoGame();
                    }
                }
            }
        }

        private void InitSceneController()
        {
            _instance = this;

            if (Utility.GetDevice() == Device.Mobile)
            {
                startModalMobile.SetActive(true);
                startModalMobile.GetComponentInChildren<Button>().onClick.AddListener(SetNextNanoGame);
            }
            else
            {
                startModalDesktop.SetActive(true);
                startModalDesktop.GetComponentInChildren<Button>().onClick.AddListener(SetNextNanoGame);
            }
        }
        
        private void SetNextNanoGame()
        {
            startModalMobile.SetActive(false);
            startModalDesktop.SetActive(false);
            
            Debug.Log("NANOGAME " +_nanoGameIndex);
            switch (_nanoGameIndex)
            {
                case 1:
                    nanoGame1.SetActive(true);
                    break;
                case 2:
                    nanoGame1.SetActive(false);
                    nanoGame2.SetActive(true);
                    break;
                case 3:
                    nanoGame2.SetActive(false);
                    nanoGame3.SetActive(true);
                    break;
                case 4:
                    nanoGame3.SetActive(false);
                    nanoGame4.SetActive(true);
                    break;
                case 5:
                    nanoGame4.SetActive(false);
                    nanoGame5.SetActive(true);
                    nanoGame5.GetComponent<NanoGame5Controller>().OnFinishedGame += OnNanoGameFinished;
                    break;
                case 6:
                    nanoGame5.GetComponent<NanoGame5Controller>().OnFinishedGame -= OnNanoGameFinished;
                    nanoGame5.SetActive(false);
                    nanoGame6.SetActive(true);
                    break;
                case 7:
                    nanoGame6.SetActive(false);
                    nanoGame7.SetActive(true);
                    break;
            }
        }
        
        private void SetEndscreen()
        {
            if (Utility.GetDevice() == Device.Desktop)
            {
                finishedModalDesktop.SetActive(true);
                //finishedModalDesktop.GetComponent<MicrogameFinished>().SetScore(correctlyAnswered, answerCount, _completedTime);
            }
            else
            {
                finishedModalMobile.SetActive(true);
                //finishedModalMobile.GetComponent<MicrogameFinished>().SetScore(correctlyAnswered, answerCount, _completedTime);
            }
        }
        
        private void OnNanoGameFinished()
        {
            toNextSceneStarted = true;
            toNextSceneStartedTime = Time.time;
        }
    }   
}
