using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Helpers;
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
    private Dictionary<string, GameObject> m_instances = new Dictionary<string, GameObject>();

    // public GameObject original;
    public GameObject pouchCell;
    public GameObject prismCell;
    public GameObject cylinderCell;
    public GameObject car;
    
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
            switch (GameState.Instance.current3dModel)
            {
                case GameState.Models.Cells:
                    InstantiateModel("prismCell", prismCell, plane.transform);
                    InstantiateModel("pouchcell", pouchCell, plane.transform);
                    InstantiateModel("cylinderCell", cylinderCell, plane.transform);
                    break;
                case GameState.Models.Pouch:
                    InstantiateModel("pouchcell", pouchCell, plane.transform);
                    break;
                case GameState.Models.Car:
                    InstantiateModel("car", car, plane.transform);
                    break;
            }
            debugText.text = "PLANE = " + plane.alignment;
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
        foreach(KeyValuePair<string,GameObject> gameObject in m_instances)
        {
            Destroy(gameObject.Value);
            m_instances.Remove(gameObject.Key);
        }
        
        debugText.text = "Drücke um zu plazieren.";
        m_PlaneManager.SetTrackablesActive(true);
        m_PlaneManager.enabled = true;
        m_instanciated = false;
    }
    
    private void InstantiateModel(string name, GameObject model, Transform plane)
    {
        position = plane.position;
        rotation = plane.rotation;
        GameObject modelGameObject = Instantiate(model);
        m_instances.Add(name, modelGameObject);
        modelGameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        modelGameObject.transform.position = position;
        modelGameObject.transform.rotation = rotation;
    }
}
