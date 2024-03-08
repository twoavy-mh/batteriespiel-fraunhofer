using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Events;
using Minigame1;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using Minigame1.Classes;
using UnityEngine.UI;

public class Timeslot : MonoBehaviour, BrokerBuyEvent.IUseBrokerBuy
{
    private List<TimeslotEntry> _brokerDay;
    private VideoPlayer _vp;
    private int _currentTimeslot;

    public BuyButton[] buyButtons = new BuyButton[3];
    public Sprite[] maps;
    public Sprite emptyMap;
    public Image mapImage;
    public int secondsPerTimeslot = 5;
    public RawImage textureRenderedTo;
    
    void Start()
    {
        SceneController.Instance.brokerBuyEvent.AddListener(UseBrokerBuyEvent);

        _vp = GetComponent<VideoPlayer>();
        _vp.frame = 0;
        _vp.targetTexture.Release();
        _vp.Prepare();
        _vp.prepareCompleted += VideoPrepared;

        _brokerDay = new List<TimeslotEntry>()
        {
            {
                new TimeslotEntry(50, 100, new ResourceInfo(25, 40, 120), new[]
                {
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[0])
            },
            {
                new TimeslotEntry(200, 250, new ResourceInfo(20, 50, 60), new[]
                {
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(0, 0, 0, "", true),
                }, maps[1])
            },
            {
                new TimeslotEntry(350, 400, new ResourceInfo(15, 60, 80), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(5, 6, 1, "australia"),
                }, maps[2])
            },
            {
                new TimeslotEntry(500, 550, new ResourceInfo(22, 45, 150), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[3])
            },
            {
                new TimeslotEntry(650, 700, new ResourceInfo(16, 85, 110), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(0, 0, 0, "", true),
                }, maps[4])
            },
            {
                new TimeslotEntry(800, 850, new ResourceInfo(18, 120, 220), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(5, 6, 1, "australia"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[5])
            },
            {
                new TimeslotEntry(950, 1000, new ResourceInfo(25, 100, 360), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(5, 6, 1, "australia"),
                    new ResourceAmount(0, 4, 0, "chile"),
                }, maps[6])
            },
            {
                new TimeslotEntry(1100, 1150, new ResourceInfo(40, 90, 160), new[]
                {
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(5, 6, 1, "australia"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[7])
            },
            {
                new TimeslotEntry(1250, 1300, new ResourceInfo(35, 90, 140), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(5, 6, 1, "australia"),
                }, maps[8])
            },
            {
                new TimeslotEntry(1400, 1450, new ResourceInfo(20, 70, 100), new[]
                {
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[9])
            },
        };

        int i = 0;
        foreach (BuyButton buyButton in buyButtons)
        {
            buyButton.SetNewBuyAmount(new Dictionary<string, int>()
            {
                { "nickle", _brokerDay[0].resourceAmount[i].nickleAmount },
                { "lithium", _brokerDay[0].resourceAmount[i].lithiumAmount },
                { "cobalt", _brokerDay[0].resourceAmount[i].cobaltAmount }
            }, _brokerDay[0].resourceAmount[i].countryKey, _brokerDay[0].resourceAmount[i].disabled);
            i++;
        }

        _currentTimeslot = 0;
    }

    private void VideoPrepared(VideoPlayer source)
    {
        StartCoroutine(StartVideo(2));
        SceneController.Instance.showWhatYouBuyEvent.Invoke(false);
    }

    private IEnumerator StartVideo(int wait)
    {
        yield return new WaitForSeconds(wait);
        textureRenderedTo.DOFade(1f, 0.5f);
        _vp.Play();
    }

    private void Update()
    {
        TimeslotEntry t = GetCurrentTimeslot();
        if (t == null)
        {
            mapImage.sprite = emptyMap;
            return;
        }
        
        if (t.start != _currentTimeslot)
        {
            _vp.Pause();
            mapImage.sprite = t.map;
            SceneController.Instance.showWhatYouBuyEvent.Invoke(true);
            StartCoroutine(EmitBuyableAgain(t.start, t.end));
            int i = 0;
            foreach (BuyButton buyButton in buyButtons)
            {
                buyButton.SetNewBuyAmount(new Dictionary<string, int>()
                {
                    { "nickle", t.resourceAmount[i].nickleAmount },
                    { "lithium", t.resourceAmount[i].lithiumAmount },
                    { "cobalt", t.resourceAmount[i].cobaltAmount }
                }, t.resourceAmount[i].countryKey, t.resourceAmount[i].disabled);
                i++;
            }

            _currentTimeslot = t.start;
        }
    }

    private IEnumerator EmitBuyableAgain(int startframe, int endframe)
    {
        yield return new WaitForSeconds(secondsPerTimeslot);
        if (!SceneController.Instance.GetFinished())
        {
            _vp.Play();    
        }
        yield return new WaitForSeconds((endframe - startframe) / _vp.frameRate);
        if (!SceneController.Instance.GetFinished())
        {
            SceneController.Instance.showWhatYouBuyEvent.Invoke(false);    
        }
    }
    
    public TimeslotEntry GetCurrentTimeslot()
    {
        int frame = (int)_vp.frame;
        TimeslotEntry currentSlot = null;

        foreach (TimeslotEntry slot in _brokerDay)
        {
            if (slot.start <= frame && slot.end >= frame)
            {
                currentSlot = slot;
                break;
            }
        }

        return currentSlot;
    }

    public void UseBrokerBuyEvent(Dictionary<string, int> boughtAmount)
    {
        TimeslotEntry t = GetCurrentTimeslot();
        NonInvestedController nic = GameObject.Find("Geld").GetComponent<NonInvestedController>();

        foreach (var (key, value) in boughtAmount)
        {
            switch (key)
            {
                case "nickle":
                    nic.Spend(value * t.resourceInfo.nicklePrice);
                    break;
                case "lithium":
                    nic.Spend(value * t.resourceInfo.lithiumPrice);
                    break;
                case "cobalt":
                    nic.Spend(value * t.resourceInfo.cobaltPrice);
                    break;
            }
        }
    }
    
    public List<TimeslotEntry> GetBrokerDay()
    {
        return _brokerDay;
    }
    
    public void OnApplicationQuit()
    {
        _vp.targetTexture.Release();
    }
}