using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorSetter : MonoBehaviour
{

    public Tailwind ImageColor;
    
    // Fake Tailwind
    void Start()
    {
        GetComponent<Image>().color = Settings.ColorMap[ImageColor];
    }
}
