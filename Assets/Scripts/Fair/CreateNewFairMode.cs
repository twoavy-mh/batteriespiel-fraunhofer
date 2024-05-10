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

        private bool _isCreating = false;
        
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => Api.Instance != null);
            GetComponent<Button>().onClick.AddListener(CreateNewFair);
        }
        
        private void CreateNewFair()
        {
            if (_isCreating) return;
            
            _isCreating = true;
            StartCoroutine(Api.Instance.CreateFair(s =>
            {
                _isCreating = false;
                if (s != null)
                {
                    PlayerPrefs.SetInt("fairCode", s.tradeShowCode);
                    GameManager.Instance.fairChangedEvent.Invoke(PlayerPrefs.GetInt("fairCode"));
                }
            }));
        }
    }
}