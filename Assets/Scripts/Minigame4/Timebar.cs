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

        private void Awake()
        {
            _remainingTime = durationInSeconds;
        }

        public void StartTimer()
        {
            transform.GetChild(0).GetComponent<RectTransform>().DOSizeDelta(new Vector2(0f, 20f), durationInSeconds).SetEase(Ease.Linear)
                .onComplete += () =>
            {
                SceneController.Instance.Die(0);
            };
        }
        
        private void SubtractFromRemainingTime()
        {
            _remainingTime--;
        }

        private int Finished()
        {
            return _remainingTime;
        }
    }    
}

