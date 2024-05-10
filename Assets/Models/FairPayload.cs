using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class FairPayload
    {
        public int tradeShowCode;
        public string id;
        
        public static explicit operator FairPayload(string v)
        {
            FairPayload casted = JsonUtility.FromJson<FairPayload>(v);
            return casted;
        }
    }
}