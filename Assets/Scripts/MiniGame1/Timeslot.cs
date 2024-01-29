using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

public class Timeslot : MonoBehaviour
{
    private List<BrokerDay> _days = new List<BrokerDay>();
    private int _currentDay = 0;
    private VideoPlayer _vp;
    
    private class ResourceInfo
    {
        public int nicklePrice;
        public int lithiumPrice;
        public int cobaltPrice;

        public ResourceInfo(int nicklePrice, int lithiumPrice, int cobaltPrice)
        {
            this.nicklePrice = nicklePrice;
            this.lithiumPrice = lithiumPrice;
            this.cobaltPrice = cobaltPrice;
        }
    }

    private class TimeslotEntry
    {
        public int start;
        public int end;
        public ResourceInfo resourceInfo;

        public TimeslotEntry(int start, int end, ResourceInfo resourceInfo)
        {
            this.start = start;
            this.end = end;
            this.resourceInfo = resourceInfo;
        }
    }

    private class BrokerDay
    {
        public int nickleAmount;
        public int lithiumAmount;
        public int cobaltAmount;
        public List<TimeslotEntry> timeslots;

        public BrokerDay(int nickleAmount, int lithiumAmount, int cobaltAmount, List<TimeslotEntry> timeslots)
        {
            this.nickleAmount = nickleAmount;
            this.lithiumAmount = lithiumAmount;
            this.cobaltAmount = cobaltAmount;
            this.timeslots = timeslots;
        }
    }
    
    void Start()
    {
        _vp = GetComponent<VideoPlayer>();
        _vp.Prepare();
        _vp.prepareCompleted += (source) =>
        {
            _vp.Play();
        };
        
        _days.Add(new BrokerDay(10, 10, 10, new List<TimeslotEntry>()
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

    // Update is called once per frame
    void Update()
    {
        TimeslotEntry t = GetCurrentTimeslot((int)_vp.frame);
        if (t != null)
        {
            Debug.Log(t.start);    
        }
        
    }
}