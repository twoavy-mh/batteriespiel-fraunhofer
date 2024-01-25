using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class PrisonBarCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(0, 0);
        rt.offsetMin = new Vector2(0, 0);

        float lineThickness = 1f;
        float oneBlock = (Screen.width - 52f * 2f) / 12f;
        Color blue = Settings.ColorMap[Tailwind.Blue3];
        
        for (int i = 0; i < 13; i++)
        {
            GameObject currentVertical = new GameObject("verticalBar" + i);
            currentVertical.transform.SetParent(transform);
            RectTransform currentVerticalRect = currentVertical.AddComponent<RectTransform>();
            RawImage img = currentVertical.AddComponent<RawImage>();
            img.color = blue;
            currentVerticalRect.anchorMax = new Vector2(0, 1);
            currentVerticalRect.anchorMin = new Vector2(0, 1);
            currentVerticalRect.pivot = new Vector2(0, 1);
            currentVerticalRect.sizeDelta = new Vector2(lineThickness, Screen.height);
            currentVerticalRect.localPosition = new Vector3(52 + oneBlock * i, 0, 0f);
            // currentVerticalRect.localScale = Vector3.one;
        }

        int iterations = 0;
        while (iterations * oneBlock < Screen.height)
        {
            iterations++;
        }
        
        
        for (int i = 0; i < iterations; i++)
        {
            GameObject currentHorizontal = new GameObject("horizontalBar" + i);
            currentHorizontal.transform.SetParent(transform);
            RectTransform currentHorizontalRect = currentHorizontal.AddComponent<RectTransform>();
            RawImage img = currentHorizontal.AddComponent<RawImage>();
            img.color = blue;
            currentHorizontalRect.anchorMax = new Vector2(0, 1);
            currentHorizontalRect.anchorMin = new Vector2(0, 1);
            currentHorizontalRect.pivot = new Vector2(0, 1);
            currentHorizontalRect.sizeDelta = new Vector2(Screen.width, lineThickness);
            currentHorizontalRect.localPosition = new Vector3(0, oneBlock * -i, 0f);
            // currentHorizontalRect.localScale = Vector3.one;
        }
    }
}