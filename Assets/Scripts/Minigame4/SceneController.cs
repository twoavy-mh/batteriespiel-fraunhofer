using System;
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
        public int finishedCount = 0;
        
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

        private void Start()
        {
            GameObject.Find("GoDown").GetComponent<Timebar>().StartTimer();
        }

        public void DroppedCorrectly(string field)
        {
            finishedCount++;
        }

        public void Die(int remainingTime)
        {
            
        }
        
    }   
}
