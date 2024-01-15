using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

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
}
