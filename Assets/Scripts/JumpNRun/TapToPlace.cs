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
    ARRaycastManager raycastManager;
    
    [SerializeField]
    ARPlaneManager planeManager;

    public GameObject resetButton;
    public float resetTime = 1.0f;
    
    List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private Boolean _instanciated = false;
    
    private Boolean _isReseting = false;
    private float _currentResetTime = 0.0f;
    
    private GameObject _instance;
    private Dictionary<string, GameObject> _instances = new Dictionary<string, GameObject>();

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
        if (Input.touchCount > 0 && !_instanciated && !_isReseting)
        {
            Touch touch = Input.GetTouch(0);
            debugText.text = touch.rawPosition.ToString();
            // Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (raycastManager.Raycast(touch.rawPosition, _hits, TrackableType.PlaneWithinPolygon)) 
            {
                debugText.text = "Raycast hit count " + _hits.Count;
                foreach(ARRaycastHit hit in _hits) 
                {
                    HandleRaycast(hit); 
                }
            }
            else
            {
                debugText.text = "No Plane touched";
            }
        }

        if (_isReseting && _currentResetTime + Time.time >= resetTime)
        {
            _isReseting = false;
            _currentResetTime = 0f;
        }
        
    }
    
    void HandleRaycast(ARRaycastHit hit)
    {
        if (_instanciated)
            return;
        if (hit.trackable is ARPlane plane)
        {
            _instanciated = true; 
            switch (GameState.Instance.current3dModel)
            {
                case GameState.Models.Cells:
                    InstantiateModel("prismCell", prismCell, plane.transform, new Vector3(0,0,.2f));
                    InstantiateModel("pouchcell", pouchCell, plane.transform);
                    InstantiateModel("cylinderCell", cylinderCell, plane.transform, new Vector3(0,0,-.1f));
                    break;
                case GameState.Models.Pouch:
                    GameObject cell = InstantiateModel("pouchcell", pouchCell, plane.transform);
                    cell.AddComponent<AnimationController>();
                    break;
                case GameState.Models.Car:
                    InstantiateModel("car", car, plane.transform, Vector3.zero, .2f);
                    break;
            }
            debugText.text = "PLANE = " + plane.alignment;
            planeManager.SetTrackablesActive(false);
            planeManager.enabled = false;
        }
        else
        {
            debugText.text = "Raycast hit count " + _hits.Count;
        }
    }
    
    public void Reset()
    {
        if (_isReseting)
            return;
        
        _isReseting = true;
        _currentResetTime = Time.time;
        
        foreach(KeyValuePair<string,GameObject> gameObject in _instances)
        {
            Destroy(gameObject.Value);
            _instances.Remove(gameObject.Key);
        }
        
        debugText.text = "Drücke um zu plazieren.";
        planeManager.SetTrackablesActive(true);
        planeManager.enabled = true;
        _instanciated = false;
    }
    
    private GameObject InstantiateModel(string name, GameObject model, Transform plane, Vector3? positionOffset = null, float scale = 0.1f)
    {
        // is positionOffset null? if yes, set position to plane.position, else set position to positionOffset
        if (positionOffset == null)
        {
            position = plane.position;
        }
        else
        {
            position = plane.position + (Vector3) positionOffset;
        }
        
        rotation = plane.rotation;
        
        GameObject modelGameObject = Instantiate(model);
        _instances.Add(name, modelGameObject);
        modelGameObject.transform.localScale = new Vector3(scale,scale,scale);
        modelGameObject.transform.position = position;
        modelGameObject.transform.rotation = rotation;

        return modelGameObject;
    }
}
