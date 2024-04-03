using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Nobi.UiRoundedCorners;
using UnityEngine.UI;

public class ButtonHeightSetter : MonoBehaviour
{
    void Start()
    {
        float multiplier = Device.Mobile == Utility.GetDevice() ? Settings.RESIZE_FACTOR : 1f;
        
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, Utility.GetDevice() == Device.Mobile ? 40 * multiplier : 60);
        rt.localScale = Vector3.one;
        ImageWithRoundedCorners iwrc = GetComponent<ImageWithRoundedCorners>();
        if (iwrc)
        {
            iwrc.radius *= multiplier;    
        }
        VerticalLayoutGroup vlg = GetComponent<VerticalLayoutGroup>();
        if (vlg)
        {
            RectOffset padding = vlg.padding;
            vlg.padding = new RectOffset((int)Math.Round(padding.left * multiplier), (int)Math.Round(padding.right * multiplier), (int)Math.Round(padding.top * multiplier), (int)Math.Round(padding.bottom * multiplier));    
        }
        
        
        GameObject child = transform.GetChild(0).gameObject;
        ImageWithRoundedCorners iwrcChild = child.GetComponent<ImageWithRoundedCorners>();
        if (iwrcChild)
        {
            iwrcChild.radius *= multiplier;    
        }
        
        HorizontalLayoutGroup hlg = child.GetComponent<HorizontalLayoutGroup>();
        if (hlg)
        {
            if (Utility.GetDevice() == Device.Mobile)
            {
                hlg.padding = new RectOffset((int)Math.Round(24 * multiplier), (int)Math.Round(24 * multiplier), (int)Math.Round(10 * multiplier), (int)Math.Round(10 * multiplier));    
            }
            else
            {
                hlg.padding = new RectOffset((int)Math.Round(24 * multiplier), (int)Math.Round(24 * multiplier), (int)Math.Round(18 * multiplier), (int)Math.Round(18 * multiplier));
            }
                
        }
    }
}
