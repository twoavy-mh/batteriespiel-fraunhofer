using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private RectTransform _rt;
    
    void Start()
    {
        _rt = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rt.anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("start");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
 
        //Raycast using the Graphics Raycaster and mouse click position
        GetComponent<GraphicRaycaster>().Raycast(eventData, results);
        foreach (RaycastResult raycastResult in results)
        {
            Debug.Log(raycastResult.gameObject.name);
        }
    }
}
