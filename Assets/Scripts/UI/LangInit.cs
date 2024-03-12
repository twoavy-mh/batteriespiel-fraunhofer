using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace UI
{
    public class LangInit : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;
            Debug.Log("Initialized LocalizationSettings");
        }
    }
}