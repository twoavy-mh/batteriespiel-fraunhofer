using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPoiContent : MonoBehaviour
{
    public CanvasGroup poiCanvasGroup;
    public GameObject closeButton;
    
    // Start is called before the first frame update
    void Start()
    {
        closeButton.GetComponent<Button>().onClick.AddListener(HidePoiCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPoiCanvas()
    {
        poiCanvasGroup.alpha = 1;
        poiCanvasGroup.blocksRaycasts = true;
        poiCanvasGroup.interactable = true;
    }

    public void HidePoiCanvas()
    {
        poiCanvasGroup.alpha = 0;
        poiCanvasGroup.blocksRaycasts = false;
        poiCanvasGroup.interactable = false;
    }
}
