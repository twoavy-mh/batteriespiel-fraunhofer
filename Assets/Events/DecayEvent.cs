using UnityEngine.Events;

namespace Events
{
    public class DecayEvent : UnityEvent<DecayInstance>
    {
        public interface IUseDecay
        {
            public void UseDecay(DecayInstance settings);
        }
    }

    public class DecayInstance
    {

        public float duration;
        public float multiplier;
        
        public DecayInstance(float duration, float multiplier)
        {
            this.duration = duration;
            this.multiplier = multiplier;
        }
    }
}