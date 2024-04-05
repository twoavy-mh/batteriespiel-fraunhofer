using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Helpers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using DG.Tweening;

public class TapToPlace : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager raycastManager;
    
    [SerializeField]
    ARPlaneManager planeManager;
    
    public CanvasGroup noticePanel;

    public GameObject resetButton;
    public float resetTime = 1.0f;
    
    List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private Boolean _instanciated = false;
    
    private Boolean _isReseting = false;
    private float _currentResetTime = 0.0f;
    
    private GameObject _instance;
    private GameObject modelGameObject;

    // public GameObject original;
    public GameObject pouchCell;
    public GameObject cells;
    public GameObject car;
    
    public Vector3 position;
    public Quaternion rotation;
    public TextMeshProUGUI debugText;

    private GameObject _displaying;
    
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

        if (_isReseting && resetTime + _currentResetTime <= Time.time)
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
            hideNoticePanel();
            switch (GameState.Instance.currentGameState.current3dModel)
            {
                case GameState.Models.Cells:
                    _displaying = InstantiateModel("cells", cells, plane.transform);
                    break;
                case GameState.Models.Pouch:
                    _displaying = InstantiateModel("pouchcell", pouchCell, plane.transform);
                    _displaying.AddComponent<AnimationController>();
                    _displaying.GetComponent<AnimationController>().ShowInitialButtons();
                    break;
                case GameState.Models.Car:
                    _displaying = InstantiateModel("car", car, plane.transform, Vector3.zero, .2f);
                    break;
            }
            debugText.text = "PLANE = " + plane.alignment;
            planeManager.SetTrackablesActive(false);
            planeManager.enabled = false;
        }
        else
        {
            debugText.text = "Raycast hit =  " + hit.trackable;
        }
    }
    
    public void Reset()
    {
        if (_isReseting)
            return;
        
        _isReseting = true;
        _currentResetTime = Time.time;

        if (_displaying)
        {
            AnimationController c = _displaying.GetComponent<AnimationController>();
            if (c != null)
            {
                c.HideButtons();
            }

            _displaying = null;
        }
        
        DestroyImmediate(modelGameObject);
        
        debugText.text = "Drücke um zu plazieren.";
        planeManager.enabled = true;
        planeManager.SetTrackablesActive(true);
        _instanciated = false;
        
        showNoticePanel();
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
        
        modelGameObject = Instantiate(model);
        modelGameObject.transform.localScale = new Vector3(scale,scale,scale);
        modelGameObject.transform.position = position;
        modelGameObject.transform.rotation = rotation;

        return modelGameObject;
    }
    
    private void showNoticePanel()
    {
        noticePanel.DOFade(1, .5f).SetEase(Ease.InCubic);
        noticePanel.interactable = true;
        noticePanel.blocksRaycasts = true;
    }
    
    private void hideNoticePanel()
    {
        noticePanel.DOFade(0, .5f).SetEase(Ease.InCubic);
        noticePanel.interactable = false;
        noticePanel.blocksRaycasts = false;
    }
}
