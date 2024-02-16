using Events;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame3
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;
        
        private int correctlyAnswered = 0;

        
        public GameObject nanoGame1Desktop; 
        public GameObject nanoGame1Mobile;
        
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
        }
        
        private void InitSceneController()
        {
            _instance = this;

            if (Utility.GetDevice() == Device.Mobile)
            {
                startModalMobile.SetActive(true);
                startModalMobile.GetComponentInChildren<Button>().onClick.AddListener(SetEndscreen);
            }
            else
            {
                startModalDesktop.SetActive(true);
                startModalDesktop.GetComponentInChildren<Button>().onClick.AddListener(SetEndscreen);
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
    }   
}
