using Events;
using UnityEngine;

namespace Minigame5
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;

        public QuizEvent quizEvent;
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
        }

        public void DroppedCorrectly(string field)
        {
            Debug.Log(field);
        }

        public void Die()
        {
            
        }
        
    }   
}
