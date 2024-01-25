using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameButtonController : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    
    [SerializeField] public int index;
    
    // Start is called before the first frame update
    void Start()
    {
        SetStatus(true);
    }
    
    public void SetStatus(bool a_Status)
    {
        Image buttonImage = GameObject.Find("ButtonImage" + index).GetComponent<Image>();;
        TMP_Text text = GameObject.Find("ButtonLabel" + index).GetComponent<TMP_Text>();
        
        if (a_Status)
        {
            text.color = Settings.ColorMap[Tailwind.Yellow3];
            buttonImage.sprite = activeSprite;
        }
        else
        {
            text.color = Settings.ColorMap[Tailwind.Azure2];
            buttonImage.sprite = inactiveSprite;
        }
    }
    
}
