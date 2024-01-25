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
        
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, Utility.GetDevice() == Device.Mobile ? 40 * multiplier : 60);
        ImageWithRoundedCorners iwrc = GetComponent<ImageWithRoundedCorners>();
        iwrc.radius *= multiplier;
        VerticalLayoutGroup vlg = GetComponent<VerticalLayoutGroup>();
        vlg.padding = new RectOffset((int)Math.Round(1 * multiplier), (int)Math.Round(1 * multiplier), (int)Math.Round(1 * multiplier), (int)Math.Round(1 * multiplier));

        GameObject child = transform.GetChild(0).gameObject;
        ImageWithRoundedCorners iwrcChild = child.GetComponent<ImageWithRoundedCorners>();
        iwrcChild.radius *= multiplier;
        HorizontalLayoutGroup hlg = child.GetComponent<HorizontalLayoutGroup>();
        hlg.padding = new RectOffset((int)Math.Round(24 * multiplier), (int)Math.Round(24 * multiplier), (int)Math.Round(10 * multiplier), (int)Math.Round(10 * multiplier));
    }
}
