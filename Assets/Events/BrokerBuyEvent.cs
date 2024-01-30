using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class BrokerBuyEvent : UnityEvent<Dictionary<string, int>>
    {
        public interface IUseBrokerBuy
        {
            public void UseBrokerBuyEvent(Dictionary<string, int> boughtAmount);
        }
    }
}