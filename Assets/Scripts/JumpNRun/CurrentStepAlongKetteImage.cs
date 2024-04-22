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
        if (GameState.Instance.GetCurrentMicrogame() == GameState.Microgames.Microgame6)
        {
            GetComponent<Image>().color = new Color(255, 255, 255, 0);
            return;
        }
        GetComponent<Image>().sprite = images[(int)GameState.Instance.GetCurrentMicrogame()].GetSprite(); 
        GetComponent<Image>().SetNativeSize();
    }
}
