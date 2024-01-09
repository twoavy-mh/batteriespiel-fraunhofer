using System;
using System.Collections.Generic;
using Events;

namespace Models
{
    public class CollectableState
    {
        public Collectable collectable;
        public int count;
        public int value;
        public int decreasesBatteryLife;
        public List<CollectableDelegate> onChange = new List<CollectableDelegate>();
        
        public CollectableState(Collectable collectable, int value, int decreasesBatteryLife = 0)
        {
            this.collectable = collectable;
            count = 0;
            this.value = value;
            this.decreasesBatteryLife = decreasesBatteryLife;
        }
        
        public void Add(int amount)
        {
            count += amount;
            foreach (CollectableDelegate action in onChange)
            {
                action.Invoke(new CollectableCollectedCallback(this.value, this.count, this.decreasesBatteryLife, this.collectable));
            }
        }

        public void AddListener(CollectableDelegate a)
        {
            onChange.Add(a);
        }
    }

    public struct CollectableCollectedCallback
    {
        public Collectable collectable;
        public int value;
        public int count;
        public int decreasesBatteryLife;
        
        public CollectableCollectedCallback(int value, int count, int decreasesBatteryLife, Collectable collectable)
        {
            this.value = value;
            this.count = count;
            this.decreasesBatteryLife = decreasesBatteryLife;
            this.collectable = collectable;
        }
    }
    
    public delegate void CollectableDelegate(CollectableCollectedCallback c);
}