using System;
using System.Collections;
using Helpers;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Fair
{
    [RequireComponent(typeof(Button))]
    public class CreateNewFairMode : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => Api.Instance != null);
            GetComponent<Button>().onClick.AddListener(CreateNewFair);
        }
        
        private void CreateNewFair()
        {
            //StartCoroutine(Api.Instance)
            PlayerPrefs.SetInt("fairCode", new Random().Next(1000, 10000));
            GameManager.Instance.fairChangedEvent.Invoke(PlayerPrefs.GetInt("fairCode"));
        }
    }
}