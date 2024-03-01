using DG.Tweening;
using Events;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace JumpNRun
{
    public class SceneController : MonoBehaviour, DieEvent.IUseDie
    {
        private static SceneController _instance;
        
        public CollectedEvent collectEvent;
        public DieEvent dieEvent;

        public GameObject dieDesktop;
        public GameObject dieMobile;
    
        public static SceneController Instance
        {
            get
            {
                if (_instance == null) Debug.Log("no SceneController yet");
                return _instance;
            }
        }

        // Start is called before the first frame update
        private void Awake()
        {
            collectEvent ??= new CollectedEvent();
            dieEvent ??= new DieEvent();
            dieEvent.AddListener(UseDie);
            _instance = this;
            gameObject.AddComponent<AutoTweenKiller>();
        }

        public void UseDie()
        {
            if (Utility.GetDevice() == Device.Desktop)
            {
                dieDesktop.SetActive(true);
            }
            else
            {
                dieMobile.SetActive(true);
            }
        }
    }   
}