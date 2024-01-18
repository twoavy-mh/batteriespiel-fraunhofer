using System.Collections;
using System.Collections.Generic;
using Events;
using Helpers;
using UnityEngine;

namespace Minigame4
{
    public class SceneController : MonoBehaviour
    {
        // Start is called before the first frame update
        private static SceneController _instance;
        
        public GameObject finishedModal;
        public HoveringOverDropzoneEvent hoveringOverDropzoneEvent;
        
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
            hoveringOverDropzoneEvent ??= new HoveringOverDropzoneEvent();
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
