using UnityEngine.Events;

namespace Events
{
    public class CompositionChangedEvent : UnityEvent<Ingredient>
    {
        public interface IUseCollectable
        {
            public void UseCollectable(Collectable c);
        }
    }

    public enum Ingredient
    {
        Aktivmaterial,
        Additive,
        Loesungsmittel
    }
}