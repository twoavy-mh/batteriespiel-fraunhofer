using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Events;
using Minigame1;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using Minigame1.Classes;

public class Timeslot : MonoBehaviour, BrokerBuyEvent.IUseBrokerBuy
{
    private List<BrokerDay> _days = new List<BrokerDay>();
    private int _currentDay = 0;
    private VideoPlayer _vp;

    public BuyButton[] buyButtons = new BuyButton[3];

    void Start()
    {
        SceneController.Instance.brokerBuyEvent.AddListener(UseBrokerBuyEvent);

        _vp = GetComponent<VideoPlayer>();
        _vp.Prepare();
        _vp.prepareCompleted += source =>
        {
            _vp.frame = 0;
            _vp.Play();
        };

        _days.Add(new BrokerDay(new[]
        {
            new ResourceAmount(1, 10, 100, "philipinen"),
            new ResourceAmount(2, 10, 100, "philipinen"),
            new ResourceAmount(3, 10, 100, "philipinen"),
        }, new List<TimeslotEntry>()
        {
            { new TimeslotEntry(50, 100, new ResourceInfo(25, 40, 120)) },
            { new TimeslotEntry(200, 250, new ResourceInfo(20, 50, 60)) },
            { new TimeslotEntry(350, 400, new ResourceInfo(15, 60, 80)) },
            { new TimeslotEntry(500, 550, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(650, 700, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(800, 850, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(950, 1000, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1100, 1150, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1250, 1300, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1400, 1450, new ResourceInfo(10, 10, 10)) },
        }));

        _days.Add(new BrokerDay(new[]
        {
            new ResourceAmount(4, 20, 200, "philipinen"),
            new ResourceAmount(5, 20, 200, "philipinen"),
            new ResourceAmount(6, 20, 200, "philipinen"),
        }, new List<TimeslotEntry>()
        {
            { new TimeslotEntry(50, 100, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(200, 250, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(350, 400, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(500, 550, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(650, 700, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(800, 850, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(950, 1000, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1100, 1150, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1250, 1300, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1400, 1450, new ResourceInfo(10, 10, 10)) },
        }));

        _days.Add(new BrokerDay(new[]
        {
            new ResourceAmount(7, 10, 10, "philipinen"),
            new ResourceAmount(8, 10, 10, "philipinen"),
            new ResourceAmount(9, 10, 10, "philipinen"),
        }, new List<TimeslotEntry>()
        {
            { new TimeslotEntry(50, 100, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(200, 250, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(350, 400, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(500, 550, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(650, 700, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(800, 850, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(950, 1000, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1100, 1150, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1250, 1300, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1400, 1450, new ResourceInfo(10, 10, 10)) },
        }));

        _days.Add(new BrokerDay(new[]
        {
            new ResourceAmount(10, 10, 10, "philipinen"),
            new ResourceAmount(10, 10, 10, "philipinen"),
            new ResourceAmount(10, 10, 10, "philipinen"),
        }, new List<TimeslotEntry>()
        {
            { new TimeslotEntry(50, 100, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(200, 250, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(350, 400, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(500, 550, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(650, 700, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(800, 850, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(950, 1000, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1100, 1150, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1250, 1300, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1400, 1450, new ResourceInfo(10, 10, 10)) },
        }));

        _days.Add(new BrokerDay(new[]
        {
            new ResourceAmount(10, 10, 10, "philipinen"),
            new ResourceAmount(10, 10, 10, "philipinen"),
            new ResourceAmount(10, 10, 10, "philipinen"),
        }, new List<TimeslotEntry>()
        {
            { new TimeslotEntry(50, 100, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(200, 250, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(350, 400, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(500, 550, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(650, 700, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(800, 850, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(950, 1000, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1100, 1150, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1250, 1300, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1400, 1450, new ResourceInfo(10, 10, 10)) },
        }));

        _days.Add(new BrokerDay(new[]
        {
            new ResourceAmount(10, 10, 10, "philipinen"),
            new ResourceAmount(10, 10, 10, "philipinen"),
            new ResourceAmount(10, 10, 10, "philipinen"),
        }, new List<TimeslotEntry>()
        {
            { new TimeslotEntry(50, 100, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(200, 250, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(350, 400, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(500, 550, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(650, 700, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(800, 850, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(950, 1000, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1100, 1150, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1250, 1300, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1400, 1450, new ResourceInfo(10, 10, 10)) },
        }));

        _days.Add(new BrokerDay(new[]
        {
            new ResourceAmount(10, 10, 10, "philipinen"),
            new ResourceAmount(10, 10, 10, "philipinen"),
            new ResourceAmount(10, 10, 10, "philipinen"),
        }, new List<TimeslotEntry>()
        {
            { new TimeslotEntry(50, 100, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(200, 250, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(350, 400, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(500, 550, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(650, 700, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(800, 850, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(950, 1000, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1100, 1150, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1250, 1300, new ResourceInfo(10, 10, 10)) },
            { new TimeslotEntry(1400, 1450, new ResourceInfo(10, 10, 10)) },
        }));
        
        int i = 0;
        foreach (BuyButton buyButton in buyButtons)
        {
            buyButton.SetNewBuyAmount(new Dictionary<string, int>()
            {
                { "nickle", _days[_currentDay].resourceAmount[i].nickleAmount },
                { "lithium", _days[_currentDay].resourceAmount[i].lithiumAmount },
                { "cobalt", _days[_currentDay].resourceAmount[i].cobaltAmount }
            }, _days[_currentDay].resourceAmount[i].countryKey);
            i++;
        }
    }

    private TimeslotEntry GetCurrentTimeslot(int frame)
    {
        BrokerDay currentDay = _days[_currentDay];
        TimeslotEntry currentSlot = null;

        foreach (TimeslotEntry slot in currentDay.timeslots)
        {
            if (slot.start <= frame && slot.end >= frame)
            {
                currentSlot = slot;
                break;
            }
        }

        return currentSlot;
    }

    private IEnumerator NextDay()
    {
        _vp.targetTexture.Release();
        yield return new WaitForSeconds(1f);
        if (_currentDay != _days.Count - 1)
        {
            _currentDay++;
            _vp.frame = 0;
        }
        else
        {
            Debug.Log("Game finished");
        }
    }

    public void UseBrokerBuyEvent(Dictionary<string, int> boughtAmount)
    {
        TimeslotEntry t = GetCurrentTimeslot((int)_vp.frame);
        NonInvestedController nic = GameObject.Find("NonInvested").GetComponent<NonInvestedController>();
        
        foreach (var (key, value) in boughtAmount)
        {
            switch (key)
            {
                case "nickle":
                    Debug.Log($"Spent {value * t.resourceInfo.nicklePrice} on {value} {key}");
                    nic.Spend(value * t.resourceInfo.nicklePrice);
                    break;
                case "lithium":
                    Debug.Log($"Spent {value * t.resourceInfo.lithiumPrice} on {value} {key}");
                    nic.Spend(value * t.resourceInfo.lithiumPrice);
                    break;
                case "cobalt":
                    Debug.Log($"Spent {value * t.resourceInfo.cobaltPrice} on {value} {key}");
                    nic.Spend(value * t.resourceInfo.cobaltPrice);
                    break;
            }
        }
    }
}