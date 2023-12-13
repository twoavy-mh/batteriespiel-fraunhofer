using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TapToPlace : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    
    [SerializeField]
    ARPlaneManager m_PlaneManager;
    
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    private Boolean m_instanciated = false;
    private GameObject m_instance;

    public GameObject original;
    public Vector3 position;
    public Quaternion rotation;
    public TextMeshProUGUI debugText;
    
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !m_instanciated)
        {
            Touch touch = Input.GetTouch(0);
            debugText.text = touch.rawPosition.ToString();
            // Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (m_RaycastManager.Raycast(touch.rawPosition, m_Hits))
             {
                // debugText.text = "Raycast hit count " + m_Hits.Count;
            //     // m_instanciated = true;
            String newString = "";
            foreach(ARRaycastHit hit in m_Hits)
            {
            // HandleRaycast(hit);
            newString += "| "  + hit.trackable;
            }
            debugText.text = newString;
             }
             else
             {
            //     //m_Hits.Clear();
                 debugText.text = "No Plane touched";
             }
        } 
    }
    
    void HandleRaycast(ARRaycastHit hit)
    {
        // if (hit.trackable is ARPlane plane)
        // {
        //     debugText.text = "PLANE = " + plane.alignment;
        //     Debug.Log($"Hit a plane with alignment {plane.alignment}");
        //     position = plane.transform.position;
        //     rotation = plane.transform.rotation;
        //
        //     m_instance = Instantiate(original);
        //     m_instance.transform.position = position;
        //     m_instance.transform.rotation = rotation;
        //     
        //     m_PlaneManager.SetTrackablesActive(false);
        //     m_PlaneManager.enabled = false;
        //     
        // }
        // else
        // {
            debugText.text = "Raycast hit count " + m_Hits.Count;
        //}
    }
}
