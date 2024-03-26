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
        
        public HoveringOverDropzoneEvent hoveringOverDropzoneEvent;
        public int finishedCount = 0;
        public int fails = 0;
        
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

        public void DroppedCorrectly(string field, int sec)
        {
            finishedCount++;
            if (finishedCount == 6)
            {
                GameObject.Find("GoDown").GetComponent<Timebar>().Finished();
                GameObject[] modals = GetComponent<RenderUiBasedOnDevice>().DoIt();
                modals[0].GetComponent<MicrogameFourFinished>().SetScore(fails, fails + finishedCount, sec);
            }
        }

        public void Die(int dur)
        {
            GameObject[] modals = GetComponent<RenderUiBasedOnDevice>().DoIt();
            modals[0].GetComponent<MicrogameFourFinished>().SetScore(fails, fails + finishedCount, dur);
        }
        
    }   
}
