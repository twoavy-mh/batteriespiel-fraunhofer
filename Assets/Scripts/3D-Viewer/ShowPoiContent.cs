using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPoiContent : MonoBehaviour
{
    public GameObject closeButton;

    private InstantiationHelper instantiationHelper;
    private CanvasGroup poiCanvasGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        // closeButton.GetComponent<Button>().onClick.AddListener(HidePoiCanvas);
        
        instantiationHelper = gameObject.AddComponent<InstantiationHelper>();
        
        GameObject poiCanvas = instantiationHelper.AddNewCanvas();
        
        Padding padding = new Padding(24f, 24f, 24f, 24f);
        GameObject poiPanel = instantiationHelper.AddNewPopupLayer(poiCanvas.GetComponent<CanvasScaler>().scaleFactor,padding, true);
        poiCanvasGroup = poiPanel.GetComponent<CanvasGroup>();
        poiPanel.transform.SetParent(poiCanvas.transform);

        GameObject textContainer = instantiationHelper.AddTextWithHeadlineAndBody();
        textContainer.transform.SetParent(poiPanel.transform);
        
        textContainer.transform.Find("Headline").GetComponent<TextMeshProUGUI>().text = "Headline";
        textContainer.transform.Find("Body").GetComponent<TextMeshProUGUI>().text = "Some Lorem Ipsum Text";
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
