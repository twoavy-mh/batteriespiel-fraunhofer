using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class HoveringOverDropzoneEvent : UnityEvent<GameObject>
    {
        public interface IUseHoveringOverDropzone
        {
            public void UseHoveringOverDropzone(GameObject hoveringOver);
        }
    }
}