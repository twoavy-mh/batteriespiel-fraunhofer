using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Minigame4
{
    public class Timebar : MonoBehaviour
    {
        public void StartTimer()
        {
            transform.GetChild(0).GetComponent<RectTransform>().DOSizeDelta(new Vector2(0f, 20f), 10f).SetEase(Ease.Linear)
                .SetDelay(2f).onComplete += () =>
            {
                SceneController.Instance.Die();
            };
        }
    }    
}

