using UnityEngine.Events;

namespace Events
{
    public class IdleEvent : UnityEvent<bool>
    {
        public interface IUseIdle
        {
            public void UseIdle();
        }
    }
}