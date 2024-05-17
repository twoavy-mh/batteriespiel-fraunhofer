using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Events;
using Helpers;
using Minigame2;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Minigame4
{
    public class Dropzone : MonoBehaviour, HoveringOverDropzoneEvent.IUseHoveringOverDropzone, CorrectDropzoneEvent.IUseCorrectDropzone
    {
        public string requires;
        public Sprite hoverSprite;
        private Sprite _normalSprite;
        
        private bool _finished = false;

        private void Start()
        {
            SceneController.Instance.hoveringOverDropzoneEvent.AddListener(UseHoveringOverDropzone);
            SceneController.Instance.correctDropzoneEvent.AddListener(UseCorrectDropzone);
            _normalSprite = GetComponent<Image>().sprite;
        }

        public void UseHoveringOverDropzone(GameObject hoveringOver)
        {
            if (_finished) return;
            
            if (hoveringOver == null)
            {
                GetComponent<Image>().sprite = _normalSprite;
                //GetComponent<ImageColorSetter>().UpdateColor(_initialColor);
                return;
            }

            if (hoveringOver.name == gameObject.name)
            {
                GetComponent<Image>().sprite = hoverSprite;
                //GetComponent<ImageColorSetter>().UpdateColor(Tailwind.Yellow1, 0.5f);
            }
            else
            {
                GetComponent<Image>().sprite = _normalSprite;
                //GetComponent<ImageColorSetter>().UpdateColor(_initialColor);
            }
        }
        
        public void UseCorrectDropzone(GameObject droppedGO)
        {
            Draggable d = droppedGO.GetComponent<Draggable>();
            if (d.requiredDropZone.Equals(requires))
            {
                if (requires.Equals("car"))
                {
                    RectTransform rt = GetComponent<RectTransform>();
                    droppedGO.GetComponent<RectTransform>()
                        .DOMove(new Vector3(rt.position.x + 200, rt.position.y + 200, rt.position.z), 0.5f);
                }
                else
                {
                     droppedGO.GetComponent<RectTransform>()
                        .DOMove(GetComponent<RectTransform>().position, 0.5f);
                }

                _finished = true;
                d.Lock();
                d.SwitchToYellow();
                Timebar t = GameObject.Find("GoDown").GetComponent<Timebar>();
                SceneController.Instance.DroppedCorrectly(requires, t.durationInSeconds - t.RemainingTime);
            }
            else
            {
                //Debug.Log(d.requiredDropZone + " != " + requires);
                //SceneController.Instance.fails++;
                //d.ResetPosition();
            }
            
            /*
             *Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
               if (d.requiredDropZone.Equals(requires))
               {
                   
             * 
             */
        }
    }
}