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
        int i = Math.Min((int)GameState.Instance.GetCurrentMicrogame(), 5);
        if (GameState.Instance.GetCurrentMicrogame() == GameState.Microgames.Microgame6 || i == 5)
        {
            GetComponent<Image>().color = new Color(255, 255, 255, 0);
            return;
        }
        GetComponent<Image>().sprite = images[i].GetSprite(); 
        GetComponent<Image>().SetNativeSize();
    }
}
