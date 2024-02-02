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

    public class MoneySpentEvent : UnityEvent<int>
    {
        public interface IUseMoneySpentEvent
        {
            public void UseMoneySpentEvent(int amount);
        }
    }
    
    public class ShowWhatYouBuyEvent : UnityEvent<bool>
    {
        public interface IUseShowWhatYouBuyEvent
        {
            public void UseShowWhatYouBuyEvent(bool show);
        }
    }
    
}