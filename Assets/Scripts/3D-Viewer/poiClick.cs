using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poiClick : MonoBehaviour
{
    public ShowPoiContent m_showPoiContentScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        m_showPoiContentScript.ShowPoiCanvas();
        Debug.Log("Clicked POI");
    }
}
