using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Events;
using Helpers;
using Minigame1;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CountdownBar : MonoBehaviour, ShowWhatYouBuyEvent.IUseShowWhatYouBuyEvent
{

    public int segments = 10;
    private float _offsetPerDay;
    private float _timePerDay;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneController.Instance.showWhatYouBuyEvent.AddListener(UseShowWhatYouBuyEvent);
        
        _offsetPerDay = GetComponent<RectTransform>().sizeDelta.x / segments;
        VideoPlayer vp = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        _timePerDay = 5 + 50 / vp.frameRate;
        for (int i = 0; i < segments; i++)
        {
            GameObject newSegment = new GameObject("Segment" + i);
            newSegment.transform.SetParent(transform);
            RectTransform rt = newSegment.AddComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0.5f, 1);
            rt.sizeDelta = new Vector2(5, GetComponent<RectTransform>().sizeDelta.y);
            rt.anchoredPosition = new Vector2(_offsetPerDay * i, 0);
            Color c = Settings.ColorMap[Tailwind.BlueUI];
            c.a = i == 0 ? 0 : 1;
            newSegment.AddComponent<Image>().color = c;
        }
    }


    public void UseShowWhatYouBuyEvent(bool show)
    {
        if (show)
        {
            RectTransform t = GetComponent<RectTransform>();
            t.DOSizeDelta(new Vector2(t.sizeDelta.x - _offsetPerDay, t.sizeDelta.y), _timePerDay).SetEase(Ease.Linear);
        }
    }
}
