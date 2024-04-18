using System;
using Events;
using TMPro;
using UnityEngine;

namespace Fair
{
    [RequireComponent(typeof(TMP_Text))]
    public class ShowCurrentKey : MonoBehaviour, FairChanged.IUseFair
    {
        private void Start()
        {
            if (PlayerPrefs.GetInt("fairCode", -1) != -1)
            {
                GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("fairCode").ToString();
            }
            GameManager.Instance.fairChangedEvent.AddListener(UseFair);
        }

        public void UseFair(int fairCode)
        {
            GetComponent<TMP_Text>().text = fairCode.ToString();
        }
    }
}