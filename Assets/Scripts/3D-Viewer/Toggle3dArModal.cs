using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class Toggle3dArModal : MonoBehaviour
{
    public Button openButton;
    public Button closeButton;
    public CanvasGroup modal;
    
    private GameObject saveAreaGameObject;
    private GameObject contentGameObject;
    
    // Start is called before the first frame update
    void Awake () {
        openButton.onClick.AddListener(OpenModal);
        closeButton.onClick.AddListener(CloseModal);
        saveAreaGameObject = transform.GetChild(0).gameObject;
        contentGameObject = saveAreaGameObject.transform.GetChild(0).gameObject;
        contentGameObject.SetActive(false);
        saveAreaGameObject.SetActive(false);
        saveAreaGameObject.SetActive(true);
        contentGameObject.SetActive(true);
        
        if (GameManager.Instance.arPopupShown)
        {
            OpenModal();
        }
    }

    void OpenModal()
    {  
        contentGameObject.SetActive(false);
        saveAreaGameObject.SetActive(false);
        saveAreaGameObject.SetActive(true);
        contentGameObject.SetActive(true);
        modal.interactable = true;
        modal.blocksRaycasts = true;
        modal.DOFade(1, GameManager.Instance.arPopupShown? 0f: .5f).SetEase(Ease.InCubic);
        
        GameManager.Instance.arPopupShown = true;
            
    }
    

    void CloseModal()
    {
        GameManager.Instance.arPopupShown = false;
        
        modal.interactable = false;
        modal.blocksRaycasts = false;
        modal.DOFade(0, .5f).SetEase(Ease.InCubic);
    }
}
