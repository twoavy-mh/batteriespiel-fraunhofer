using System;
using Helpers;
using UnityEngine;

public class RectSizer : MonoBehaviour
{
    
    public float widthScreendesign;
    public float heightScreendesign;

    public float xDiff;
    public float yDiff;
    public float zDiff;
    
    private void Start()
    {
        if (Utility.GetDevice() == Device.Mobile)
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(widthScreendesign * Settings.RESIZE_FACTOR, heightScreendesign * Settings.RESIZE_FACTOR);
            rt.localPosition = new Vector3(rt.localPosition.x + xDiff, rt.localPosition.y + yDiff, rt.localPosition.z + zDiff);
        }
    }
}