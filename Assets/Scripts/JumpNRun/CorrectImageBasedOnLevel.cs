using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CorrectImageBasedOnLevel : MonoBehaviour
{
    [Serializable]
    public struct ImageText
    {
        public Sprite s;
        public string t;
    }

    [SerializeField] public ImageText[] sprites;
    
    void Start()
    {
        int i = (int)GameState.Instance.GetCurrentMicrogame();
        if (i == 5) return;
        //transform.GetChild(0).GetComponent<TMP_Text>().text = sprites[i].t;
        transform.GetChild(1).GetComponent<Image>().sprite = sprites[i].s;
    }
}
