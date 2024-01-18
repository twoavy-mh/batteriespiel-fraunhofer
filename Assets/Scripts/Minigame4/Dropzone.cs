using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Events;
using Helpers;
using Minigame2;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Minigame4
{
    public class Dropzone : MonoBehaviour, IDropHandler, HoveringOverDropzoneEvent.IUseHoveringOverDropzone
    {
        public string requires;
        private Tailwind _initialColor;

        private void Start()
        {
            SceneController.Instance.hoveringOverDropzoneEvent.AddListener(UseHoveringOverDropzone);
            _initialColor = GetComponent<ImageColorSetter>().ImageColor;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (LayerMask.NameToLayer("Player").Equals(eventData.pointerDrag.layer))
            {
                Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
                if (d.requiredDropZone.Equals(requires))
                {
                    SceneController.Instance.DroppedCorrectly(requires);
                    eventData.pointerDrag.GetComponent<RectTransform>().DOMove(GetComponent<RectTransform>().position, 0.5f);
                    d.Lock();
                    d.SwitchToYellow();
                    GetComponent<ImageColorSetter>().UpdateColor(_initialColor);
                }
                else
                {
                    d.ResetPosition();
                }
            }
        }


        public void UseHoveringOverDropzone(GameObject hoveringOver)
        {
            if (hoveringOver == null)
            {
                GetComponent<ImageColorSetter>().UpdateColor(_initialColor);
                return;
            }
            
            if (hoveringOver.name == gameObject.name)
            {
                GetComponent<ImageColorSetter>().UpdateColor(Tailwind.Yellow1, 0.5f);
            }
            else
            {
                GetComponent<ImageColorSetter>().UpdateColor(_initialColor);
            }
        }
    }   
}