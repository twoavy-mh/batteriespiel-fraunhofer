using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Events;
using Helpers;
using Minigame1;
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
    public BuyButton[] buyButtonsMobile = new BuyButton[3];
    private BuyButton[] _useButtons = new BuyButton[3];
    public Sprite[] maps;
    public Sprite emptyMap;
    public Image mapImage;
    private int _secondsPerTimeslot = 6;
    public RawImage textureRenderedTo;
    
    private int _playedRounds = 0;
    public bool startGame = false;

    private void Awake()
    {
        _useButtons = Utility.GetDevice() == Device.Desktop ? buyButtons : buyButtonsMobile;
    }

    void Start()
    {
        SceneController.Instance.brokerBuyEvent.AddListener(UseBrokerBuyEvent);

        _vp = GetComponent<VideoPlayer>();
        _vp.targetTexture.Release();
        _vp.Prepare();
        _vp.prepareCompleted += VideoPrepared;

        _brokerDay = new List<TimeslotEntry>()
        {
            {
                new TimeslotEntry(50, 100, new ResourceInfo(25, 120, 40), new[]
                {
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[0])
            },
            {
                new TimeslotEntry(200, 250, new ResourceInfo(20, 60, 50), new[]
                {
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(0, 0, 0, "out_of_order", true),
                }, maps[1])
            },
            {
                new TimeslotEntry(350, 400, new ResourceInfo(15, 80, 60), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(5, 6, 1, "australia"),
                }, maps[2])
            },
            {
                new TimeslotEntry(500, 550, new ResourceInfo(22, 150, 45), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[3])
            },
            {
                new TimeslotEntry(650, 700, new ResourceInfo(16, 110, 85), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(0, 0, 0, "out_of_order", true),
                }, maps[4])
            },
            {
                new TimeslotEntry(800, 850, new ResourceInfo(18, 220, 120), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(5, 6, 1, "australia"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[5])
            },
            {
                new TimeslotEntry(950, 1000, new ResourceInfo(25, 360, 100), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(5, 6, 1, "australia"),
                    new ResourceAmount(0, 4, 0, "chile"),
                }, maps[6])
            },
            {
                new TimeslotEntry(1100, 1150, new ResourceInfo(40, 160, 90), new[]
                {
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(5, 6, 1, "australia"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[7])
            },
            {
                new TimeslotEntry(1250, 1300, new ResourceInfo(35, 140, 90), new[]
                {
                    new ResourceAmount(0, 0, 4, "kongo"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(5, 6, 1, "australia"),
                }, maps[8])
            },
            {
                new TimeslotEntry(1400, 1450, new ResourceInfo(20, 100, 70), new[]
                {
                    new ResourceAmount(14, 0, 1, "indonesia"),
                    new ResourceAmount(0, 4, 0, "chile"),
                    new ResourceAmount(8, 0, 0, "philippines"),
                }, maps[9])
            },
        };

        int i = 0;
        foreach (BuyButton buyButton in _useButtons)
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
        Debug.Log($"prepared {_vp.source}");
        StartCoroutine(StartVideo());
    }

    private IEnumerator StartVideo()
    {
        yield return new WaitUntil(() => startGame);
        SceneController.Instance.showWhatYouBuyEvent.Invoke(false);
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
            _playedRounds++;
            mapImage.sprite = t.map;
            StartCoroutine(EmitBuyableAgain(t.start, t.end));
            int i = 0;
            foreach (BuyButton buyButton in _useButtons)
            {
                buyButton.SetNewBuyAmount(new Dictionary<string, int>()
                {
                    { "nickle", t.resourceAmount[i].nickleAmount },
                    { "lithium", t.resourceAmount[i].lithiumAmount },
                    { "cobalt", t.resourceAmount[i].cobaltAmount }
                }, t.resourceAmount[i].countryKey, t.resourceAmount[i].disabled);
                i++;
            }
            SceneController.Instance.showWhatYouBuyEvent.Invoke(true);
            _currentTimeslot = t.start;
        }
    }

    private IEnumerator EmitBuyableAgain(int startframe, int endframe)
    {
        float i = (endframe - startframe) / 2f / _vp.frameRate;
        yield return new WaitForSeconds(_secondsPerTimeslot - i);
        if (!SceneController.Instance.GetFinished())
        {
            _vp.Play();    
        }
        yield return new WaitForSeconds(i);
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
    
    public int GetPlayedRounds()
    {
        return _playedRounds;
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