using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ApiError
    {
        public string status;
        public string message;
        
        public static explicit operator ApiError(string v)
        {
            ApiError casted = JsonUtility.FromJson<ApiError>(v);
            return casted;
        }
    }
}