using System;
using Events;
using Helpers;
using TMPro;
using UnityEngine;

namespace JumpNRun
{
    public class TargetDisplay : MonoBehaviour, CollectedEvent.IUseCollectable
    {
        private int _collected = 0;
        private TMP_Text _text;
        private string _pattern = "{0} / 5";
        private string _patternLast = "{0}";

        private void Start()
        {
            SceneController.Instance.collectEvent.AddListener(UseCollectable);
            _text = GetComponent<TMP_Text>();
            Debug.Log("aaaaaaaa");
            Debug.Log((int)GameState.Instance.GetCurrentMicrogame());
            Debug.Log("bbbbbbbb");
            _text.text =
                string.Format(Math.Min((int)GameState.Instance.GetCurrentMicrogame(), 5) < 5 ? _pattern : _patternLast,
                    _collected);
        }

        public void UseCollectable(Collectable c)
        {
            if (c == Collectable.LevelSpecific)
            {
                _collected++;
                if (Math.Min((int)GameState.Instance.GetCurrentMicrogame(), 5) < 5)
                {
                    _text.text = string.Format(_pattern, _collected);
                }
                else
                {
                    _text.text = string.Format(_patternLast, _collected);
                }
            }
        }
    }
}