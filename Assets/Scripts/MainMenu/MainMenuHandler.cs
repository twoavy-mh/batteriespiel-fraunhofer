using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helpers;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    public Button m_MainMenuButton;
    public Transform m_MainMenuButtonIcon;
    private RectTransform _menuItemsTransform;
    public GameObject slideShowState;
    public GameObject regularState;

    public GameObject InfoLangFairMode;
    
    public GameObject infoCanvas;
    public Button infoButton;
    public Button infoCloseButton;
    
    public TMP_Text finishedGameCounter;

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
        infoButton.onClick.AddListener(OpenInfoModal);
        infoCloseButton.onClick.AddListener(CloseInfoModal);
        
        string finishedGames = "";
        try
        {
            finishedGames = GameState.Instance.currentGameState.results.Where(state => state.unlocked)
                .Count().ToString();
        }
        catch (NullReferenceException)
        {
            finishedGames = "0";
        }
        
        finishedGameCounter.text = $"{finishedGames}/5";
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
            Transform gameProgressGO = GameObject.Find("GameProgressOutline").transform;
            gameProgressGO.localPosition = new Vector2(gameProgressGO.localPosition.x + 100f, gameProgressGO.localPosition.y);
            InfoLangFairMode.transform.SetSiblingIndex(4);
            InfoLangFairMode.transform.GetChild(0).transform.localPosition = new Vector2(-740f + InfoLangFairMode.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x, -530f);
            m_MainMenuButton.gameObject.SetActive(false);
            ShowDesktopMenu();
        }
        
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
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(menuItems.GetComponent<RectTransform>());
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
    
    private void OpenInfoModal()
    {
        infoCanvas.GetComponentInChildren<CanvasGroup>().alpha = 0f;
        infoCanvas.SetActive(true);
        infoCanvas.GetComponentInChildren<CanvasGroup>().DOFade(1, .5f).SetEase(Ease.InCubic);
    }
    
    private void CloseInfoModal()
    {
        infoCanvas.GetComponentInChildren<CanvasGroup>().DOFade(0, .5f).SetEase(Ease.InCubic).OnComplete(() =>
        { 
            infoCanvas.SetActive(false);
        });
    }
}