using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuHandler : MonoBehaviour
{
    public Button m_MainMenuButton;
    public Transform m_MainMenuButtonIcon;
    public RectTransform m_MainMenuTransform;
    public Canvas m_ParentCanvas;
    
    private Boolean menuOpen = false;

    private float MENU_HEIGHT = 226f;
    private float PADDING_LEFT = 16f;
    private float PADDING_RIGHT = 34f;
    
    private InstantiationHelper instantiationHelper;
    
    // Start is called before the first frame update
    void Start()
    {
        instantiationHelper = gameObject.AddComponent<InstantiationHelper>();
        
        m_MainMenuButton.onClick.AddListener(toggleMenu);
        SetMenuTransform();
        
        
        for (int index = 1; index <= 5; index++)
        {
            GameObject btn = instantiationHelper.AddMicroGameButton(index);
            btn.transform.SetParent( GameObject.Find("MicroGames").transform);
            btn.transform.localScale = Vector3.one;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void toggleMenu()
    {
        menuOpen = !menuOpen;
        if (menuOpen)
        {
            ShowMenuTransform();
        }
        else
        {
            HideMenuTransform();
        }
    }

    private void SetMenuTransform()
    {
        m_MainMenuTransform.anchorMin = new Vector2(1, 0);
        m_MainMenuTransform.anchorMax = new Vector2(1, 0);
        m_MainMenuTransform.pivot = new Vector2(0, 0);
        
        HideMenuTransform();
    }

    private void HideMenuTransform()
    {
        float paddingBottom = (Screen.height - Screen.safeArea.y - Screen.safeArea.height)/m_ParentCanvas.scaleFactor;
        
        float localXPosition = ((Screen.width - Screen.safeArea.x - Screen.safeArea.width)/m_ParentCanvas.scaleFactor) + PADDING_RIGHT;
        m_MainMenuTransform.DOAnchorPos( new Vector2(-localXPosition, paddingBottom),.5f).SetEase(Ease.InCubic);
        m_MainMenuTransform.DOSizeDelta( new Vector2(( (Screen.width-Screen.safeArea.x)/m_ParentCanvas.scaleFactor ) + PADDING_RIGHT, MENU_HEIGHT + paddingBottom),.5f).SetEase(Ease.InCubic);

        m_MainMenuButtonIcon.DORotate(new Vector3(0,0,180), .5f).SetEase(Ease.InCubic); 
        
        CanvasGroup[] childCanvasGroups = m_MainMenuTransform.GetComponentsInChildren<CanvasGroup>();
        foreach (CanvasGroup canvasGroup in childCanvasGroups)
        {
            canvasGroup.DOFade(0, .5f).SetEase(Ease.InCubic);
        }
    }

    private void ShowMenuTransform()
    {
        float paddingBottom = (Screen.height - Screen.safeArea.y - Screen.safeArea.height)/m_ParentCanvas.scaleFactor;
        
        float localXPosition = ((Screen.width - Screen.safeArea.x)/m_ParentCanvas.scaleFactor)-PADDING_LEFT;
        m_MainMenuTransform.DOAnchorPos(new Vector2(-localXPosition, paddingBottom),.5f).SetEase(Ease.InCubic);
        m_MainMenuTransform.DOSizeDelta( new Vector2(( (Screen.width-Screen.safeArea.x)/m_ParentCanvas.scaleFactor ) + PADDING_RIGHT, MENU_HEIGHT + paddingBottom),.5f).SetEase(Ease.InCubic);

        m_MainMenuButtonIcon.DORotate(new Vector3(0,0,0), .5f).SetEase(Ease.InCubic); 

        CanvasGroup[] childCanvasGroups = m_MainMenuTransform.GetComponentsInChildren<CanvasGroup>();
        foreach (CanvasGroup canvasGroup in childCanvasGroups)
        {
            canvasGroup.DOFade(1, .5f).SetEase(Ease.InCubic);
        }
    }
}
