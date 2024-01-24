using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class ImageHeightWidthSetter : MonoBehaviour
{
    public int mobileWidth;
    public int mobileHeight;
    public int desktopWidth;
    public int desktopHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        float multiplier = Settings.RESIZE_FACTOR;
        
        RectTransform rt = GetComponent<RectTransform>();
        Vector2 size;
        if (Utility.GetDevice() == Device.Mobile)
        {
            size = new Vector2(mobileWidth * multiplier, mobileHeight * multiplier);
        }
        else
        {
            size = new Vector2(desktopWidth, desktopHeight);
        }
        
        rt.sizeDelta = size;
    }
}
