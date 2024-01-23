using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Toggle3dArModal : MonoBehaviour
{
    public Button openButton;
    public Button closeButton;
    public CanvasGroup modal;
    
    // Start is called before the first frame update
    void Awake () {
        openButton.onClick.AddListener(OpenModal);
        closeButton.onClick.AddListener(CloseModal);
    }

    void OpenModal()
    {
        modal.interactable = true;
        modal.blocksRaycasts = true;
        modal.DOFade(1, .5f).SetEase(Ease.InCubic);
    }
    

    void CloseModal()
    {
        modal.interactable = false;
        modal.blocksRaycasts = false;
        modal.DOFade(0, .5f).SetEase(Ease.InCubic);
    }
}
