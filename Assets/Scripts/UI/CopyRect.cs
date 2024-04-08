using System;
using UnityEngine;

namespace UI
{
    public class CopyRect : MonoBehaviour
    {
        private RectTransform _o;
        public RectTransform reference;
        public float width;
        public float height;
        
        private void Start()
        {
            _o = GetComponent<RectTransform>();
            Vector2 sd = reference.sizeDelta;
            if (width > 0)
            {
                sd.x = width;
            }
            if (height > 0)
            {
                sd.y = height;
            }
            _o.sizeDelta = sd;
            _o.anchoredPosition = reference.anchoredPosition;
            _o.anchorMax = reference.anchorMax;
            _o.anchorMin = reference.anchorMin;
            _o.pivot = reference.pivot;
            _o.offsetMax = reference.offsetMax;
            _o.offsetMin = reference.offsetMin;
        }
    }
}