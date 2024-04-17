using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class CurrentStepAlongKetteImage : MonoBehaviour
{
    
    [SerializeField]
    public MultilangImage[] images = new MultilangImage[5];
    
    void Start()
    {
        GetComponent<Image>().sprite = images[(int)GameState.Instance.GetCurrentMicrogame()].GetSprite(); 
        GetComponent<Image>().SetNativeSize();
    }
}
