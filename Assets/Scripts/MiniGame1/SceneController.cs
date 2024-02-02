using System.Collections;
using System.Collections.Generic;
using Events;
using Helpers;
using UnityEngine;

namespace Minigame1
{
    public class SceneController : MonoBehaviour
    {
        public ShowWhatYouBuyEvent showWhatYouBuyEvent;
        public BrokerBuyEvent brokerBuyEvent;
        public MoneySpentEvent moneySpentEvent;
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
            showWhatYouBuyEvent ??= new ShowWhatYouBuyEvent();
            brokerBuyEvent ??= new BrokerBuyEvent();
            moneySpentEvent ??= new MoneySpentEvent();
            _instance = this;
        }
    }   
}
