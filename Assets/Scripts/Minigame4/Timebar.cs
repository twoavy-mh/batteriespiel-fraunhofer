using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Minigame4
{
    public class Timebar : MonoBehaviour
    {
        public int durationInSeconds = 20;
        private int _remainingTime;

        public int RemainingTime => _remainingTime;
        
        private void Awake()
        {
            _remainingTime = durationInSeconds;
            InvokeRepeating(nameof(DecreaseTime), 1f, 1f);
        }

        public void StartTimer()
        {
            transform.GetChild(0).GetComponent<RectTransform>().DOSizeDelta(new Vector2(0f, 20f), durationInSeconds)
                .SetEase(Ease.Linear);
        }

        public void Finished()
        {
            DOTween.KillAll();
            CancelInvoke();
        }
        
        private void DecreaseTime()
        {
            _remainingTime--;
            if (_remainingTime == 0)
            {
                Finished();
                SceneController.Instance.Die(durationInSeconds);
            }
        }
    }    
}

