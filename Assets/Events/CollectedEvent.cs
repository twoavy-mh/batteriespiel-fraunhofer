using UnityEngine.Events;

namespace Events
{
    public class CollectedEvent : UnityEvent<Collectable>
    {
        public interface IUseCollectable
        {
            public void UseCollectable(Collectable c);
        }
    }

    public enum Collectable
    {
        Lithium,
        BlueLightning,
        YellowLightning,
    }
}