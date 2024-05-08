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
    public GameObject all;
    
    void Start()
    {
        int i = Math.Min((int)GameState.Instance.GetCurrentMicrogame(), 5);
        if (i >= 5)
        {
            all.SetActive(true);
            RectTransform rt = transform.parent.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x * 1.5f, rt.sizeDelta.y);
            transform.GetChild(1).gameObject.SetActive(false);
            return;
        };
        //transform.GetChild(0).GetComponent<TMP_Text>().text = sprites[i].t;
        transform.GetChild(1).GetComponent<Image>().sprite = sprites[i].s;
    }
}
