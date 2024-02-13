using System;
using UnityEngine;
using Helpers;
using Nobi.UiRoundedCorners;
using UnityEngine.UI;

public class ButtonSquareSizeSetter : MonoBehaviour
{
    void Start()
    {
        float multiplier = Settings.RESIZE_FACTOR;
        
        /**
         * Get all components *
         */
        RectTransform outlineRt = GetComponent<RectTransform>();
        ImageWithRoundedCorners outlineIwrc = GetComponent<ImageWithRoundedCorners>();
        
        ImageWithRoundedCorners backgroundIwrc = transform.GetChild(0).GetComponent<ImageWithRoundedCorners>();
        
        // RectTransform iconRt = transform.GetChild(1).GetComponent<RectTransform>();
        
        /**
         * OUTLINE SETTER *
         */

        Vector2 sizeDelta;
        if (Utility.GetDevice() == Device.Mobile)
        {
            sizeDelta = new Vector2(40 * multiplier, 40 * multiplier);
        }
        else
        {
            sizeDelta = new Vector2(60, 60);
        }
        
        outlineRt.sizeDelta = sizeDelta;
        outlineIwrc.radius *= multiplier;
        
        /** 
         * BACKGROUND SETTER *
         */ 
        
        backgroundIwrc.radius *= multiplier;
        
        /**
         * ICON SETTER *
         */

        // iconRt.sizeDelta = sizeDelta;
    }
}
