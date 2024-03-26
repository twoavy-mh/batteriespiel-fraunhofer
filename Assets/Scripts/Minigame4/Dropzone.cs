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
    public class Dropzone : MonoBehaviour, IDropHandler, HoveringOverDropzoneEvent.IUseHoveringOverDropzone
    {
        public string requires;
        public Sprite hoverSprite;
        private Sprite _normalSprite;

        private void Start()
        {
            SceneController.Instance.hoveringOverDropzoneEvent.AddListener(UseHoveringOverDropzone);
            _normalSprite = GetComponent<Image>().sprite;
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerDrag.layer);
            if (LayerMask.NameToLayer("Player").Equals(eventData.pointerDrag.layer))
            {
                Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
                if (d.requiredDropZone.Equals(requires))
                {
                    if (requires.Equals("car"))
                    {
                        RectTransform rt = GetComponent<RectTransform>();
                        eventData.pointerDrag.GetComponent<RectTransform>().DOMove(new Vector3(rt.position.x + 200, rt.position.y + 200, rt.position.z), 0.5f);
                    }
                    else
                    {
                        eventData.pointerDrag.GetComponent<RectTransform>().DOMove(GetComponent<RectTransform>().position, 0.5f);    
                    }
                    
                    d.Lock();
                    d.SwitchToYellow();
                    SceneController.Instance.DroppedCorrectly(requires);
                }
                else
                {
                    Debug.Log(d.requiredDropZone + " != " + requires);
                    d.ResetPosition();
                }
            }
        }


        public void UseHoveringOverDropzone(GameObject hoveringOver)
        {
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
    }   
}