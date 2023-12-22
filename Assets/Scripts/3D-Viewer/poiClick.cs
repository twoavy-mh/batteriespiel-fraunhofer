using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poiClick : MonoBehaviour
{
    private ShowPoiContent showPoiContentScript;
    // Start is called before the first frame update
    void Start()
    {
        showPoiContentScript = GameObject.Find("MAIN").GetComponent<ShowPoiContent>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        showPoiContentScript.ShowPoiCanvas("POI Inhalt", "Die ist ein Text \n" + "leider ohne Inhalt \n"+"aber mit Zeilenumbruch");
        Debug.Log("Clicked POI");
    }
}
