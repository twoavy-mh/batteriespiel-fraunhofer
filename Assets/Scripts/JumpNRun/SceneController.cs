using DG.Tweening;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace JumpNRun
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;
        
        public CollectedEvent collectEvent;
        public DieEvent dieEvent;
    
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
            _instance = this;
            DataStore.Instance.Init();
            gameObject.AddComponent<AutoTweenKiller>();
        }
    }   
}