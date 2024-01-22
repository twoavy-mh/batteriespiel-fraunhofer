using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressRingController : MonoBehaviour
{

    public Image fillingRing;
    public TMP_Text progressText;
    
    // Start is called before the first frame update
    public void StartAnimation(float scored)
    {
        StartCoroutine(Helpers.Utility.AnimateAnything(1f, 0f, scored, ((progress, start, end) =>
        {
            fillingRing.fillAmount = Mathf.Lerp(start, end, progress) / 100f;
            progressText.text = Mathf.RoundToInt(Mathf.Lerp(start, end, progress)).ToString();
        })));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
