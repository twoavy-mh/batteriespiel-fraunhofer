using Minigame4;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class CorrectDropzoneEvent : UnityEvent<GameObject>
    {
        public interface IUseCorrectDropzone
        {
            public void UseCorrectDropzone(GameObject droppedGO);
        }
    }
}