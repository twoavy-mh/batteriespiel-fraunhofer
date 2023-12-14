using UnityEngine.Events;

namespace Events
{
    public class RegenerateEvent : UnityEvent<RegenerationInstance>
    {
        public interface IUseRegeneration
        {
            public void UseRegeneration(RegenerationInstance settings);
        }
    }

    public class RegenerationInstance
    {

        public float duration;
        public float multiplier;
        
        public RegenerationInstance(float duration, float multiplier)
        {
            this.duration = duration;
            this.multiplier = multiplier;
        }
    }
}