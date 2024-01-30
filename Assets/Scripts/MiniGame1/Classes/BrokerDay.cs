using System.Collections.Generic;

namespace Minigame1.Classes
{
    public class ResourceInfo
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
    
    public class ResourceAmount
    {
        public int nickleAmount;
        public int lithiumAmount;
        public int cobaltAmount;
        public string countryKey;
        public bool disabled;

        public ResourceAmount(int nickleAmount, int lithiumAmount, int cobaltAmount, string countryKey, bool disabled = false)
        {
            this.nickleAmount = nickleAmount;
            this.lithiumAmount = lithiumAmount;
            this.cobaltAmount = cobaltAmount;
            this.countryKey = countryKey;
            this.disabled = disabled;
        }
    }

    public class TimeslotEntry
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

    public class BrokerDay
    {
        public ResourceAmount[] resourceAmount;
        public List<TimeslotEntry> timeslots;

        public BrokerDay(ResourceAmount[] resourceAmount, List<TimeslotEntry> timeslots)
        {
            this.resourceAmount = resourceAmount;
            this.timeslots = timeslots;
        }
    }
}