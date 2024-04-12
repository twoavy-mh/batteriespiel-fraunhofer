using System.Collections;
using DG.Tweening;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Minigame2
{
 public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _initialPosition;
    private Canvas canvas;

    private RectTransform _rt;
    public string displayName;
    public string requiredDropZone;
    private bool _finished = false;
    
    public bool fakeBelow = false;

    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
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
        
        Utility.GetTranslatedText(displayName, s => transform.GetChild(1).GetComponent<TMP_Text>().text = s);
    }

    private void InfoButton()
    {
        if (Utility.GetDevice() == Device.Desktop)
        {
            SceneController.Instance.modalGoDekstop.SetActive(true);
            SceneController.Instance.modalGoDekstop.GetComponent<ModalManager>().SetText(displayName, $"{displayName}_info");    
        }
        else
        {
            SceneController.Instance.modalGoMobile.SetActive(true);
            SceneController.Instance.modalGoMobile.GetComponent<ModalManager>().SetText(displayName, $"{displayName}_info");
        }
        
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
            _rt.localPosition += (Vector3)eventData.delta / canvas.scaleFactor;
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
            
            //_rt.position = _initialPosition;
            _rt.DOMove(_initialPosition, 0.5f, false);
        }
    }

    public void ResetPosition()
    {
        _rt.DOMove(_initialPosition, 0.5f, false);
    }

    public void Lock()
    {
        transform.GetChild(2).GetComponent<CanvasGroup>().ignoreParentGroups = true;
        _finished = true;
        Debug.Log("finished");
    }

    public void SwitchToWhite()
    {
        transform.GetChild(1).GetComponent<TMP_Text>().color = Settings.ColorMap[Tailwind.Blue1];
        GetComponent<Image>().color = Settings.ColorMap[Tailwind.Blue1];
        
        /*if (!fakeBelow)
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
        }*/
        
    }
}   
}