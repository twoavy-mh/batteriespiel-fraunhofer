using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;


namespace Minigame4
{
    public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector3 _initialPosition;
        private Canvas canvas;

        private RectTransform _rt;
        public Sprite battery;
        public string displayName;
        public string requiredDropZone;
        private bool _finished = false;
        private GameObject _modalGo;

        private GraphicRaycaster _raycaster;

        public bool fakeBelow = false;

        void Start()
        {
            GameObject c = GameObject.Find("Test");
            canvas = c.GetComponent<Canvas>();
            _raycaster = c.GetComponent<GraphicRaycaster>();

            transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().sprite = battery;

            GameObject[] all = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject go in all)
            {
                if (go.name.Equals("Modal"))
                {
                    _modalGo = go;
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

            transform.GetChild(1).GetComponent<TMP_Text>().text =
                LocalizationSettings.StringDatabase.GetLocalizedString(displayName);
        }

        private void InfoButton()
        {
            _modalGo.GetComponent<ModalManager>().SetText(displayName, $"{displayName}_info");
            _modalGo.SetActive(true);
        }

        private IEnumerator ToFake(Color tmpColor)
        {
            yield return new WaitForEndOfFrame();
            GetComponent<Image>().color = tmpColor;
        }

        public void OnDrag(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            _raycaster.Raycast(eventData, results);

            bool hit = false;
            GameObject hitGo = null;
            foreach (RaycastResult raycastResult in results)
            {
                if (raycastResult.gameObject.layer == LayerMask.NameToLayer("Dropzone"))
                {
                    hit = true;
                    hitGo = raycastResult.gameObject;
                }
            }

            if (hit)
            {
                SceneController.Instance.hoveringOverDropzoneEvent.Invoke(hitGo);
            }
            else
            {
                SceneController.Instance.hoveringOverDropzoneEvent.Invoke(null);
            }

            if (!_finished && !fakeBelow)
            {
                _rt.localPosition += (Vector3)eventData.delta / canvas.scaleFactor;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Drag begin");
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
            Debug.Log("Drag end");
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
            _finished = true;
            Debug.Log("finished");
        }

        public void SwitchToYellow()
        {
            transform.GetChild(1).GetComponent<TMP_Text>().color = Settings.ColorMap[Tailwind.Yellow4];
            GetComponent<Image>().color = Settings.ColorMap[Tailwind.Blue1];

            if (!fakeBelow)
            {
                CanvasGroup cg = transform.GetChild(2).GetComponent<CanvasGroup>();
                CanvasGroup cg2 = transform.GetChild(3).GetComponent<CanvasGroup>();
                StartCoroutine(Utility.AnimateAnything(0.5f, 1f, 0f,
                    (progress, start, end) => { cg.alpha = Mathf.Lerp(start, end, progress); }, () =>
                    {
                        cg.alpha = 0f;
                        cg.interactable = false;
                        cg.blocksRaycasts = false;
                        cg.gameObject.SetActive(false);
                        cg2.gameObject.SetActive(true);
                        StartCoroutine(StartSecondCoroutine(cg2));
                    }));
            }
        }

        private IEnumerator StartSecondCoroutine(CanvasGroup cg)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Utility.AnimateAnything(0.5f, 0f, 1f,
                (progress, start, end) => { cg.alpha = Mathf.Lerp(start, end, progress); }, () =>
                {
                    cg.alpha = 1f;
                    cg.interactable = true;
                    cg.blocksRaycasts = false;
                }));
        }
    }
}