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
        transform.GetChild(0).GetComponent<TMP_Text>().text = sprites[(int)GameState.Instance.GetCurrentMicrogame()].t;
        transform.GetChild(1).GetComponent<Image>().sprite = sprites[(int)GameState.Instance.GetCurrentMicrogame()].s;
    }
}
