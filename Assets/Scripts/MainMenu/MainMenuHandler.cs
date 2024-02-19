using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helpers;

public class MainMenuHandler : MonoBehaviour
{
    public Button m_MainMenuButton;
    public Transform m_MainMenuButtonIcon;
    private RectTransform _menuItemsTransform;
    public GameObject slideShowState;
    public GameObject regularState;

    private Boolean menuOpen = false;

    private Canvas _parentCanvas;

    private float MENU_HEIGHT = Settings.RESIZE_FACTOR * 226f;
    private float PADDING_LEFT = Settings.RESIZE_FACTOR * 16f;
    private float PADDING_RIGHT = Settings.RESIZE_FACTOR * 34f;

    private float DESKTOP_MENU_HEIGHT = 220f;
    private float DESKTOP_PADDING_LEFT = 70f;
    private float DESKTOP_PADDING_RIGHT = 70f;

    private InstantiationHelper instantiationHelper;

    // Start is called before the first frame update
    void Start()
    {
        _parentCanvas = transform.GetComponent<Canvas>();

        instantiationHelper = gameObject.AddComponent<InstantiationHelper>();

        GameObject menuItems = GameObject.Find("MenuItems");
        _menuItemsTransform = GameObject.Find("MenuBorder").GetComponent<RectTransform>();
        instantiationHelper.AddMenuItemsLayout(menuItems);

        SetMenuTransform();
        if (Utility.GetDevice() == Device.Mobile)
        {
            m_MainMenuButton.onClick.AddListener(toggleMenu);
            HideMenuTransform();
        }
        else
        {
            m_MainMenuButton.gameObject.SetActive(false);
            ShowDesktopMenu();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(menuItems.GetComponent<RectTransform>());

        if ((int)GameState.Instance.GetCurrentMicrogame() == 0)
        {
            slideShowState.SetActive(true);
            regularState.SetActive(false);
        }
        else
        {
            slideShowState.SetActive(false);
            regularState.SetActive(true);
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
        _menuItemsTransform.anchorMin = new Vector2(1, 0);
        _menuItemsTransform.anchorMax = new Vector2(1, 0);
        _menuItemsTransform.pivot = new Vector2(0, 0);
    }

    private void HideMenuTransform()
    {
        float paddingBottom = (Screen.height - Screen.safeArea.y - Screen.safeArea.height) / _parentCanvas.scaleFactor;
        float localXPosition =
            ((Screen.width - Screen.safeArea.x - Screen.safeArea.width) / _parentCanvas.scaleFactor) + PADDING_RIGHT;
        _menuItemsTransform.DOAnchorPos(new Vector2(-localXPosition, paddingBottom), .5f).SetEase(Ease.InCubic);
        _menuItemsTransform
            .DOSizeDelta(
                new Vector2(((Screen.width - Screen.safeArea.x) / _parentCanvas.scaleFactor) + PADDING_RIGHT,
                    MENU_HEIGHT + paddingBottom), .5f).SetEase(Ease.InCubic);

        m_MainMenuButtonIcon.DORotate(new Vector3(0, 0, 180), .5f).SetEase(Ease.InCubic);

        CanvasGroup[] childCanvasGroups = _menuItemsTransform.GetComponentsInChildren<CanvasGroup>();
        foreach (CanvasGroup canvasGroup in childCanvasGroups)
        {
            canvasGroup.DOFade(0, .5f).SetEase(Ease.InCubic);
        }
    }

    private void ShowMenuTransform()
    {
        float paddingBottom = (Screen.height - Screen.safeArea.y - Screen.safeArea.height) / _parentCanvas.scaleFactor;
        float localXPosition = ((Screen.width - Screen.safeArea.x) / _parentCanvas.scaleFactor) - PADDING_LEFT;
        _menuItemsTransform.DOAnchorPos(new Vector2(-localXPosition, paddingBottom), .5f).SetEase(Ease.InCubic);
        _menuItemsTransform
            .DOSizeDelta(
                new Vector2(((Screen.width - Screen.safeArea.x) / _parentCanvas.scaleFactor) + PADDING_RIGHT,
                    MENU_HEIGHT + paddingBottom), .5f).SetEase(Ease.InCubic);

        m_MainMenuButtonIcon.DORotate(new Vector3(0, 0, 0), .5f).SetEase(Ease.InCubic);

        CanvasGroup[] childCanvasGroups = _menuItemsTransform.GetComponentsInChildren<CanvasGroup>();
        foreach (CanvasGroup canvasGroup in childCanvasGroups)
        {
            canvasGroup.DOFade(1, .5f).SetEase(Ease.InCubic);
        }
    }

    private void ShowDesktopMenu()
    {
        float paddingBottom = (Screen.height - Screen.safeArea.y - Screen.safeArea.height) / _parentCanvas.scaleFactor;
        float localXPosition = ((Screen.width - Screen.safeArea.x) / _parentCanvas.scaleFactor) - DESKTOP_PADDING_LEFT;
        _menuItemsTransform.anchoredPosition = new Vector2(-localXPosition, paddingBottom);
        _menuItemsTransform.sizeDelta =
            new Vector2(
                ((Screen.width - Screen.safeArea.x) / _parentCanvas.scaleFactor) - DESKTOP_PADDING_RIGHT -
                DESKTOP_PADDING_LEFT,
                DESKTOP_MENU_HEIGHT + paddingBottom);

        CanvasGroup[] childCanvasGroups = _menuItemsTransform.GetComponentsInChildren<CanvasGroup>();
        foreach (CanvasGroup canvasGroup in childCanvasGroups)
        {
            canvasGroup.alpha = 1;
        }
    }
}