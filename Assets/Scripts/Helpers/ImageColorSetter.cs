using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[ExecuteInEditMode]
public class ImageColorSetter : MonoBehaviour
{

    public Tailwind ImageColor;
    [Range(0f, 1f)]
    public float opacity = 1f;
    
    // Fake Tailwind
    void Start()
    {
        Color tmp = Settings.ColorMap[ImageColor];
        tmp.a = opacity;
        GetComponent<Image>().color = tmp;
    }

    public void UpdateColor(Tailwind newColor, float newOpacity = 1f, bool doTween = false)
    {
        ImageColor = newColor;
        Color tmp = Settings.ColorMap[ImageColor];
        tmp.a = newOpacity;
        
        if (doTween)
        {
            GetComponent<Image>().DOColor(tmp, 0.5f);
        }
        else
        {
            GetComponent<Image>().color = tmp;
        }
    }
}
