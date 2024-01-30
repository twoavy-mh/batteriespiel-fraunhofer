using System.Collections;
using System.Collections.Generic;
using Events;
using Helpers;
using UnityEngine;

namespace Minigame1
{
    public class SceneController : MonoBehaviour
    {

        public BrokerBuyEvent brokerBuyEvent;
        private static SceneController _instance;
        
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
            brokerBuyEvent ??= new BrokerBuyEvent();
            _instance = this;
        }
    }   
}
