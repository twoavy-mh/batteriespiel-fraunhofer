using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class SlideshowController : MonoBehaviour
    {
        public Button[] actionButtons;
        private List<Action> _actions = new List<Action>();
        private RectTransform _rt;
        
        private void Awake()
        {
            _rt = transform.GetChild(0).GetComponent<RectTransform>();
            _actions.Add(() => MoveTo(-1184));
            _actions.Add(() => MoveTo(-1184 * 2));
            _actions.Add(() => SceneManager.LoadScene("JumpNRun"));

            int i = 0;
            foreach (Button button in actionButtons)
            {
                int t = i;
                button.onClick.AddListener(() => OnActionButtonClick(t));
                i++;
            }
        }

        private void OnActionButtonClick(int idx)
        {
            _actions[idx].Invoke();
        }

        private void MoveTo(float targetX)
        {
            _rt.DOAnchorPos(new Vector2(targetX, _rt.anchoredPosition.y), 0.5f);
        }
    }
}