using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class OnlyShowIfNoPlayPref : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("uuid").Empty())
        {
            GetComponent<CanvasGroup>().alpha = 1f;
        }
    }
}
