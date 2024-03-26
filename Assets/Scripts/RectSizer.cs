using System;
using Helpers;
using Nobi.UiRoundedCorners;
using UnityEngine;

public class RectSizer : MonoBehaviour
{
    
    public float widthScreendesign;
    public float heightScreendesign;

    public float xDiff;
    public float yDiff;
    public float zDiff;

    public bool adjustRoundedCorners = false;
    public bool multiplyByResizeFactor = false;
    
    private void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (Utility.GetDevice() == Device.Mobile)
        {
            float usedWidth = widthScreendesign == 0 ? GetComponent<RectTransform>().sizeDelta.x * (multiplyByResizeFactor ? Settings.RESIZE_FACTOR : 1) : widthScreendesign * Settings.RESIZE_FACTOR;
            float usedHeight = heightScreendesign == 0 ? GetComponent<RectTransform>().sizeDelta.y * (multiplyByResizeFactor ? Settings.RESIZE_FACTOR : 1): heightScreendesign * Settings.RESIZE_FACTOR;
            
            // Debug.Log(usedWidth + ", " + usedHeight);
            rt.sizeDelta = new Vector2(usedWidth, usedHeight);
            rt.localPosition = new Vector3(rt.localPosition.x + xDiff, rt.localPosition.y + yDiff, rt.localPosition.z + zDiff);
        }

        if (!adjustRoundedCorners) return;
        ImageWithRoundedCorners i = GetComponent<ImageWithRoundedCorners>();
        if (i != null)
        {
            i.radius = rt.sizeDelta.x / 2;
            i.Refresh();
        }
    }
}