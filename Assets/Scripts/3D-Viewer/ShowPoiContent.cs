using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ShowPoiContent : MonoBehaviour
{
    public CanvasGroup poiCanvasGroup;
    public RectTransform textRectTransform;
    public SelfTranslatingText headline;
    public SelfTranslatingText body;
    public Button closeButton; 
    
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(HidePoiCanvas);
    }

    public void ShowPoiCanvas(string a_Headline, string a_Body)
    {
        headline.translationKey = a_Headline;
        body.translationKey = a_Body;
        
        poiCanvasGroup.DOFade(1, .5f).SetEase(Ease.InCubic);
        poiCanvasGroup.blocksRaycasts = true;
        poiCanvasGroup.interactable = true;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(textRectTransform);
    }

    public void HidePoiCanvas()
    {
        poiCanvasGroup.DOFade(0, .5f).SetEase(Ease.InCubic);
        poiCanvasGroup.blocksRaycasts = false;
        poiCanvasGroup.interactable = false;
    }
}
