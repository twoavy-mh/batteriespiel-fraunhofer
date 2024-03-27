using System;
using System.Collections.Generic;
using Helpers;
using Models;
using TMPro;
using UnityEngine;

namespace Minigame2
{
    public class SceneController : MonoBehaviour
    {
        // Start is called before the first frame update
        private static SceneController _instance;
        private Dictionary<string, bool> _dropzones = new Dictionary<string, bool>();
        public GameObject modalGoDekstop;
        public GameObject modalGoMobile;
        
        public DateTime startTime;

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
            startTime = DateTime.Now;
            _dropzones.Add("kathode", false);
            _dropzones.Add("anode", false);
            _dropzones.Add("lith_discharge", false);
            _dropzones.Add("separator", false);
            _dropzones.Add("lith_charge", false);
            _dropzones.Add("electrolyte", false);

            _instance = this;
        }

        public void DroppedCorrectly(string field)
        {
            _dropzones[field] = true;
            if (_dropzones["kathode"] && _dropzones["anode"] && _dropzones["lith_discharge"] &&
                _dropzones["separator"] && _dropzones["lith_charge"] && _dropzones["electrolyte"])
            {
                GameObject[] all = GetComponent<RenderUiBasedOnDevice>().DoIt();
                foreach (GameObject o in all)
                {
                    Debug.Log(o.name);
                }
                
                all[0].GetComponent<MicrogameTwoFinished>().SetScore(fails, fails + 6);
            }
        }
    }
}