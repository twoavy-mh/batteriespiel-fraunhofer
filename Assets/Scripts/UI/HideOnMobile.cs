using System;
using UnityEngine;

namespace UI
{
    public class HideOnMobile : MonoBehaviour
    {
        private void Awake()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                gameObject.SetActive(false);
            }
        }
    }
}