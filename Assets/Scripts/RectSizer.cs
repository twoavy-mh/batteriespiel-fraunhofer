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
    
    private void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (Utility.GetDevice() == Device.Mobile)
        {
            rt.sizeDelta = new Vector2(widthScreendesign * Settings.RESIZE_FACTOR, heightScreendesign * Settings.RESIZE_FACTOR);
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