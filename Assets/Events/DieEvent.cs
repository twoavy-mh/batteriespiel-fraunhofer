using UnityEngine.Events;

namespace Events
{
    public class DieEvent : UnityEvent
    {
        public interface IUseDie
        {
            public void UseDie();
        }
    }
}