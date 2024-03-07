using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Minigame3.Classes;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame3
{
    public class NanoGameSelectController : MonoBehaviour
    {
        public event Action OnPrevButton;
        public event Action OnNextButton;
        public event Action OnPlayButton;

        public Button prevButton;
        public Button nextButton;
        public Button playButton;
        public SelfTranslatingText playButtonLabel;
        
        public SelfTranslatingText title;
        public SelfTranslatingText body;
        public Image image;

        public GameObject currentIndexGameObject;

        public void OnEnable()
        {
            prevButton.onClick.AddListener(PrevNanoGame);
            nextButton.onClick.AddListener(NextNanoGame);
            playButton.onClick.AddListener(SetNextNanoGame);
        }

        public void OnDisable()
        {
            prevButton.onClick.RemoveListener(PrevNanoGame);
            nextButton.onClick.RemoveListener(NextNanoGame);
            playButton.onClick.RemoveListener(SetNextNanoGame);
        }

        public void SetContent(NanoGameContent nanoGameContent, int index)
        {
            title.translationKey = nanoGameContent.titleKey;
            body.translationKey = nanoGameContent.bodyKey;
            image.sprite = nanoGameContent.nanoImage;
            playButtonLabel.translationKey = nanoGameContent.playButtonLabelKey;
            SetCurrentIndex(index);
        }
        
        private void SetCurrentIndex(int index)
        {
            ImageColorSetter[] imageColorSetters = currentIndexGameObject.GetComponentsInChildren<ImageColorSetter>();
            
            for (int icsIndex = 0; icsIndex < imageColorSetters.Length; icsIndex++)
            {
                float opacity = icsIndex <= index? 1f:0f;
                imageColorSetters[icsIndex].opacity = opacity;
                imageColorSetters[icsIndex].UpdateColor(Tailwind.Yellow3, opacity);
            }
        }
        
        private void PrevNanoGame()
        {
            OnPrevButton?.Invoke();
        }
        
        private void NextNanoGame()
        {
            OnNextButton?.Invoke();
        }
        
        private void SetNextNanoGame()
        {
            OnPlayButton?.Invoke();
        }
    }
}
