using System;
using UnityEngine;

namespace Fair
{
    public class FairController : MonoBehaviour
    {
        private static FairController _instance;
        
        public static FairController Instance
        {
            get
            {
                if (_instance == null) Debug.Log("no GameManager yet");
                return _instance;
            }
        }
        
        private void Awake()
        {
            _instance = this;
        }
    }
}