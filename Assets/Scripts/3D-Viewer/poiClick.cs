using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiClick : MonoBehaviour
{
    public string headlineKey;
    public string bodyKey;
    
    private ShowPoiContent showPoiContentScript;
    // Start is called before the first frame update
    void Start()
    {
        showPoiContentScript = GameObject.Find("PoiOverlay")?.GetComponent<ShowPoiContent>();
    }

    private void OnMouseDown()
    {
        showPoiContentScript.ShowPoiCanvas(headlineKey, bodyKey);
    }
}
