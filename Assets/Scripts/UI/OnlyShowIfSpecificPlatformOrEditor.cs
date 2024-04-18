using System;
using UnityEngine;

namespace UI
{
    public class OnlyShowIfSpecificPlatformOrEditor : MonoBehaviour
    {
        public string platform;
        public bool showInEditor = true;

        private void Start()
        {
            if (SystemInfo.operatingSystem.Contains(platform) || (Application.isEditor && showInEditor))
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}