using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    
    [SerializeField]
    ARPlaneManager m_PlaneManager;

    public GameObject resetButton;
    
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    private Boolean m_instanciated = false;
    private GameObject m_instance;

    public GameObject original;
    public Vector3 position;
    public Quaternion rotation;
    public TextMeshProUGUI debugText;
    
    void Awake()
    {
        resetButton.GetComponent<Button>().onClick.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !m_instanciated)
        {
            Touch touch = Input.GetTouch(0);
            debugText.text = touch.rawPosition.ToString();
            // Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (m_RaycastManager.Raycast(touch.rawPosition, m_Hits, TrackableType.PlaneWithinPolygon)) 
            {
                debugText.text = "Raycast hit count " + m_Hits.Count;
                foreach(ARRaycastHit hit in m_Hits) 
                {
                    HandleRaycast(hit); 
                }
            }
            else
            {
                debugText.text = "No Plane touched";
            }
        } 
    }
    
    void HandleRaycast(ARRaycastHit hit)
    {
        if (m_instanciated)
            return;
        if (hit.trackable is ARPlane plane)
        {
            m_instanciated = true; 
            debugText.text = "PLANE = " + plane.alignment;
        //     Debug.Log($"Hit a plane with alignment {plane.alignment}");
            position = plane.transform.position;
            rotation = plane.transform.rotation;
             
            m_instance = Instantiate(original);
            m_instance.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            m_instance.transform.position = position;
            m_instance.transform.rotation = rotation;
             
            m_PlaneManager.SetTrackablesActive(false);
            m_PlaneManager.enabled = false;
        }
        else
        {
            debugText.text = "Raycast hit count " + m_Hits.Count;
        }
    }
    
    public void Reset()
    {
        if (m_instance != null)
        {
            Destroy(m_instance);
            m_instance = null;
        }
        debugText.text = "Drücke um zu plazieren.";
        m_PlaneManager.SetTrackablesActive(true);
        m_PlaneManager.enabled = true;
        m_instanciated = false;
    }
}
