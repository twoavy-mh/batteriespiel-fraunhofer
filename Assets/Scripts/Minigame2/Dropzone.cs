using System.Collections;
using System.Collections.Generic;
using Minigame2;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Minigame2
{
    public class Dropzone : MonoBehaviour, IDropHandler
    {
        public string requires;

        public void OnDrop(PointerEventData eventData)
        {
            if (LayerMask.NameToLayer("Player").Equals(eventData.pointerDrag.layer))
            {
                Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
                if (d.requiredDropZone.Equals(requires))
                {
                    SceneController.Instance.DroppedCorrectly(requires);
                    eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                    d.Lock();
                    d.SwitchToWhite();
                }
                else
                {
                    SceneController.Instance.fails++;
                    d.ResetPosition();
                }
            }
        }
    }   
}