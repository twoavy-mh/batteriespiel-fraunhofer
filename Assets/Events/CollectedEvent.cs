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
        LevelSpecific,
        BlueLightning,
        YellowLightning,
    }
}