using System;
using Events;
using Helpers;
using Minigame3.Classes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Minigame3
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;

        public int startGame = 5;
        private int _nanoGameIndex;

        public float timerToNanoGameSelect = 2f;
        private float toNanoGameSelectStartedTime;
        private bool toNanoGameSelectStarted = false;
        
        private int correctlyAnswered = 0;
        
        [SerializeField]
        public NanoGameContent[] nanoGameContents = new NanoGameContent[7];
        
        public GameObject startModalDesktop;
        public GameObject startModalMobile;
        
        public GameObject nextNanoGameModalDesktop;
        public GameObject nextNanoGameModalMobile;
        
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
            if (toNanoGameSelectStarted)
            {
                if (toNanoGameSelectStartedTime + timerToNanoGameSelect < Time.time)
                {
                    toNanoGameSelectStarted = false;
                    if (_nanoGameIndex >= 7)
                    {
                        SetEndscreen();
                    }
                    else
                    {
                        _nanoGameIndex++;
                        SetNanoGameSelect();
                    }
                }
            }
        }

        
        private void NextNanoGame()
        {
            if (_nanoGameIndex < 7)
            {
                _nanoGameIndex++;
                nextNanoGameModalMobile.GetComponent<NanoGameSelectController>().SetContent(nanoGameContents[_nanoGameIndex - 1],_nanoGameIndex - 1);
            }
        }
        
        private void PrevNanoGame()
        {
            if (_nanoGameIndex > 1)
            {
                _nanoGameIndex--;
                nextNanoGameModalMobile.GetComponent<NanoGameSelectController>().SetContent(nanoGameContents[_nanoGameIndex - 1], _nanoGameIndex - 1);
            }
        }
        
        private void InitSceneController()
        {
            _instance = this;

            if (Utility.GetDevice() == Device.Mobile)
            {
                startModalMobile.SetActive(true);
                startModalMobile.GetComponentsInChildren<Button>()[1].onClick.AddListener(HideStartModal);
            }
            else
            {
                startModalDesktop.SetActive(true);
                startModalDesktop.GetComponentsInChildren<Button>()[0].onClick.AddListener(HideStartModal);
            }
        }
        
        private void HideStartModal()
        {
            if (Utility.GetDevice() == Device.Mobile)
            {
                startModalMobile.SetActive(false);
                startModalMobile.GetComponentInChildren<Button>().onClick.RemoveListener(HideStartModal);
            }
            else
            {
                startModalDesktop.SetActive(false);
                startModalDesktop.GetComponentInChildren<Button>().onClick.RemoveListener(HideStartModal);
            }
            
            NanoGameSelect();
        }
        
        private void SetNanoGameSelect()
        {
            foreach (NanoGameContent nanoGameContent in nanoGameContents)
            {
                nanoGameContent.nanoGameObject.SetActive(false);
            }
            NanoGameSelect();
        }

        private void NanoGameSelect()
        {
            if (Utility.GetDevice() == Device.Mobile)
            {
                NanoGameSelectController ngsCtrl = nextNanoGameModalMobile.GetComponentInChildren<NanoGameSelectController>();
                ngsCtrl.SetContent(nanoGameContents[_nanoGameIndex - 1], _nanoGameIndex - 1);
                nextNanoGameModalMobile.SetActive(true);
                
                ngsCtrl.OnPrevButton += PrevNanoGame;
                ngsCtrl.OnNextButton += NextNanoGame;
                ngsCtrl.OnPlayButton += SetNextNanoGame;
            }
            else
            {
                NanoGameSelectController ngsCtrl = nextNanoGameModalDesktop.GetComponentInChildren<NanoGameSelectController>();
                ngsCtrl.SetContent(nanoGameContents[_nanoGameIndex - 1], _nanoGameIndex - 1);
                nextNanoGameModalDesktop.SetActive(true);
                Debug.Log(ngsCtrl.name);
                ngsCtrl.OnPrevButton += PrevNanoGame;
                ngsCtrl.OnNextButton += NextNanoGame;
                ngsCtrl.OnPlayButton += SetNextNanoGame;
            }
        }

        private void HideNanoGameSelect()
        {
            if (Utility.GetDevice() == Device.Mobile)
            {
                NanoGameSelectController ngsCtrl = nextNanoGameModalMobile.GetComponent<NanoGameSelectController>();
                nextNanoGameModalMobile.SetActive(false);
                
                ngsCtrl.OnPrevButton -= PrevNanoGame;
                ngsCtrl.OnNextButton -= NextNanoGame;
                ngsCtrl.OnPlayButton -= SetNextNanoGame;
            }
            else
            {
                NanoGameSelectController ngsCtrl = nextNanoGameModalDesktop.GetComponent<NanoGameSelectController>();
                nextNanoGameModalDesktop.SetActive(false);
                
                ngsCtrl.OnPrevButton -= PrevNanoGame;
                ngsCtrl.OnNextButton -= NextNanoGame;
                ngsCtrl.OnPlayButton -= SetNextNanoGame;
            }
        }
        
        private void SetNextNanoGame()
        {
            HideNanoGameSelect();
            
            nanoGameContents[_nanoGameIndex - 1].nanoGameObject.SetActive(true);
            switch (_nanoGameIndex)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    nanoGameContents[_nanoGameIndex - 1].nanoGameObject.GetComponent<NanoGame5Controller>().OnFinishedGame += OnNanoGameFinished;
                    break;
                case 6:
                    break;
                case 7:
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
            nanoGameContents[_nanoGameIndex - 1].nanoGameObject.GetComponent<NanoGame5Controller>().OnFinishedGame -= OnNanoGameFinished;
            nanoGameContents[_nanoGameIndex - 1].solved = true;
            toNanoGameSelectStarted = true;
            toNanoGameSelectStartedTime = Time.time;
        }
    }   
}
