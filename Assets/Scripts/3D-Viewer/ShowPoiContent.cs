using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPoiContent : MonoBehaviour
{
    private InstantiationHelper instantiationHelper;
    private TextMeshProUGUI headline;
    private TextMeshProUGUI body;
    private CanvasGroup poiCanvasGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        instantiationHelper = gameObject.AddComponent<InstantiationHelper>();
        
        GameObject poiCanvas = instantiationHelper.AddNewCanvas();
        
        float scaleFactor = poiCanvas.GetComponent<CanvasScaler>().referenceResolution.x / Screen.width;
        Padding padding = new Padding(24f, 24f, 24f, 24f);
        GameObject poiPanel = instantiationHelper.AddNewPopupLayer(scaleFactor, padding, true);
        poiCanvasGroup = poiPanel.GetComponent<CanvasGroup>();
        poiPanel.transform.SetParent(poiCanvas.transform);

        GameObject textContainer = instantiationHelper.AddTextWithHeadlineAndBody();
        textContainer.transform.SetParent(poiPanel.transform);
        headline = textContainer.transform.Find("Headline").GetComponent<TextMeshProUGUI>();
        body = textContainer.transform.Find("Body").GetComponent<TextMeshProUGUI>();

        GameObject closeGameObject = instantiationHelper.AddNewButton("close", "justIcon", "close", "", false);
        closeGameObject.transform.SetParent(poiPanel.transform);
        closeGameObject.GetComponent<Button>().onClick.AddListener(HidePoiCanvas);
    }

    public void ShowPoiCanvas(string a_Headline, string a_Body)
    {
        headline.text = a_Headline;
        body.text = a_Body;
        
        poiCanvasGroup.DOFade(1, .5f).SetEase(Ease.InCubic);
        poiCanvasGroup.blocksRaycasts = true;
        poiCanvasGroup.interactable = true;
    }

    public void HidePoiCanvas()
    {
        poiCanvasGroup.DOFade(0, .5f).SetEase(Ease.InCubic);
        poiCanvasGroup.blocksRaycasts = false;
        poiCanvasGroup.interactable = false;
    }
}
