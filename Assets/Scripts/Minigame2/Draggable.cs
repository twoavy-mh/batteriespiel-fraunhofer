using System.Collections;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _initialPosition;

    private RectTransform _rt;
    public string displayName;
    public string requiredDropZone;
    private bool _finished = false;
    public GameObject modalGo;
    
    public bool fakeBelow = false;

    void Start()
    {
        GameObject[] all = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject go in all)
        {
            if (go.name.Equals("Modal"))
            {
                modalGo = go;
            }
        }
        _rt = GetComponent<RectTransform>();
        _initialPosition = new Vector3(_rt.position.x, _rt.position.y, _rt.position.z);

        if (fakeBelow)
        {
            Color tmpColor = Settings.ColorMap[Tailwind.Blue1];
            tmpColor.a = 0.5f;
            transform.GetChild(1).GetComponent<TMP_Text>().color = tmpColor;
            StartCoroutine(ToFake(tmpColor));
            Destroy(transform.GetChild(2).gameObject);
        }
        else
        {
            transform.GetChild(2).GetComponent<Button>().onClick.AddListener(InfoButton);
        }
        
        transform.GetChild(1).GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString(displayName);
    }

    private void InfoButton()
    {
        modalGo.GetComponent<ModalManager>().SetText(displayName, $"{displayName}_info");
        modalGo.SetActive(true);
    }
    
    private IEnumerator ToFake(Color tmpColor)
    {
        yield return new WaitForEndOfFrame();
        GetComponent<Image>().color = tmpColor;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!_finished && !fakeBelow)
        {
            float screenAdjustedX = eventData.delta.x * (1920f / Screen.width);
            float screenAdjustedY = eventData.delta.y * (1080f / Screen.height);
            _rt.anchoredPosition += new Vector2(screenAdjustedX, screenAdjustedY);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!fakeBelow)
        {
            transform.GetChild(2).GetComponent<Button>().interactable = false;
            transform.GetChild(2).GetComponent<Image>().raycastTarget = false;
        }
        if (!_finished && !fakeBelow)
        {
            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!fakeBelow)
        {
            transform.GetChild(2).GetComponent<Button>().interactable = true;
            transform.GetChild(2).GetComponent<Image>().raycastTarget = true;
        }
        if (!_finished && !fakeBelow)
        {
            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<Image>().raycastTarget = true;
        }
    }

    public void ResetPosition()
    {
        _rt.position = _initialPosition;
    }

    public void Lock()
    {
        _finished = true;
    }

    public void SwitchToWhite()
    {
        transform.GetChild(1).GetComponent<TMP_Text>().color = Settings.ColorMap[Tailwind.Blue1];
        GetComponent<Image>().color = Settings.ColorMap[Tailwind.Blue1];
        
        if (!fakeBelow)
        {
            CanvasGroup cg = transform.GetChild(2).GetComponent<CanvasGroup>();
            StartCoroutine(Utility.AnimateAnything(0.5f, 1f, 0f, (progress, start, end) =>
            {
                cg.alpha = Mathf.Lerp(start, end, progress);
            }, () =>
            {
                cg.alpha = 0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }));    
        }
        
    }
}