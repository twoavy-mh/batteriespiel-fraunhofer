using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Events;
using Helpers;
using Minigame1;
using Minigame1.Classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour, MoneySpentEvent.IUseMoneySpentEvent, ShowWhatYouBuyEvent.IUseShowWhatYouBuyEvent
{
    public GameObject circlePrefab;
    private NonInvestedController _nic;

    public Dictionary<string, int> buyAmount = new Dictionary<string, int>()
    {
        { "nickle", 3 },
        { "lithium", 5 },
        { "cobalt", 3 }
    };

    private string _countryKey;

    private bool _disabled = false;
    private bool _broke = false;
    private bool _bought = false;

    private bool Broke
    {
        get => _broke;
        set
        {
            _broke = value;
            if (_broke && !_disabled)
            {
                transform.GetChild(1).GetComponent<TMP_Text>().color = Settings.ColorMap[Tailwind.Blue5];
                transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.InOutSine);
                transform.parent.GetComponent<Image>().DOColor(Settings.ColorMap[Tailwind.Blue5], 0.5f);
                // GetComponent<Button>().interactable = true;
            }
            else
            {
                // GetComponent<Button>().interactable = false;
                transform.GetChild(1).GetComponent<TMP_Text>().color = Settings.ColorMap[Tailwind.Blue1];
                transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.InOutSine);
                transform.parent.GetComponent<Image>()
                    .DOColor(Settings.ColorMap[_disabled ? Tailwind.Blue5 : Tailwind.Blue1], 0.5f);
            }
        }
    }

    void Start()
    {
        SceneController.Instance.moneySpentEvent.AddListener(UseMoneySpentEvent);
        SceneController.Instance.showWhatYouBuyEvent.AddListener(UseShowWhatYouBuyEvent);
        GetComponent<Button>().onClick.AddListener(Buy);
        _nic = GameObject.Find("Geld").GetComponent<NonInvestedController>();
    }

    public void SetNewBuyAmount(Dictionary<string, int> newAmount, string countryKey, bool disabled)
    {
        _bought = false;
        buyAmount = newAmount;
        if (countryKey.Equals(""))
        {
            transform.GetChild(1).GetComponent<TMP_Text>().text = "";
        }
        else
        {
            Utility.GetTranslatedText(countryKey, s => transform.GetChild(1).GetComponent<TMP_Text>().text = s.ToUpper());    
        }
        
        _countryKey = countryKey;
        _disabled = disabled;

        CheckBroke();
        KillChildren();

        transform.parent.GetComponent<Image>()
            .DOColor(Settings.ColorMap[_disabled ? Tailwind.Blue5 : Tailwind.Blue1], 0.5f);


        int i = 0;
        foreach (var (key, value) in newAmount.Reverse())
        {
            if (value == 0)
            {
                continue;
            }

            GameObject c = Instantiate(circlePrefab, Vector3.zero, Quaternion.identity);
            c.transform.SetParent(transform.GetChild(0));
            c.transform.localScale = Vector3.one;
            RectTransform rt = c.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(1f, 1f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.pivot = new Vector2(1f, 1f);
            c.transform.localPosition = new Vector3(i * (Helpers.Utility.GetDevice() == Device.Desktop ? -30 : -18 * Settings.RESIZE_FACTOR) - 15, 20, 0);
            CountCircleController cc = c.GetComponent<CountCircleController>();
            cc.SetCount(value);
            cc.SetColor(GetColor(key));
            i++;
        }
    }

    private Tailwind GetColor(string key)
    {
        switch (key)
        {
            case "nickle":
                return Tailwind.Orange4;
            case "lithium":
                return Tailwind.Green3;
            case "cobalt":
                return Tailwind.Violet2;
            default:
                return Tailwind.Violet2;
        }
    }

    private void KillChildren()
    {
        int count = transform.GetChild(0).childCount;
        for (int x = 0; x < count; x++)
        {
            Destroy(transform.GetChild(0).GetChild(x).gameObject);
        }
    }

    private void Buy()
    {
        if (_disabled || Broke)
        {
            return;
        }

        _bought = true;
        _disabled = true;
        if (GameObject.Find("Video Player").GetComponent<Timeslot>().GetCurrentTimeslot() != null)
        {
            SceneController.Instance.brokerBuyEvent.Invoke(buyAmount);
            KillChildren();
            SetNewBuyAmount(new Dictionary<string, int>()
            {
                { "nickle", 0 },
                { "lithium", 0 },
                { "cobalt", 0 }
            }, _countryKey, _disabled);
            transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.InOutSine);
        }
    }

    public void UseMoneySpentEvent(int amount)
    {
        CheckBroke();
    }

    private void CheckBroke()
    {
        TimeslotEntry t = GameObject.Find("Video Player").GetComponent<Timeslot>().GetCurrentTimeslot();
        if (t == null)
        {
            return;
        }

        int sum = buyAmount["nickle"] * t.resourceInfo.nicklePrice +
                  buyAmount["lithium"] * t.resourceInfo.lithiumPrice +
                  buyAmount["cobalt"] * t.resourceInfo.cobaltPrice;
        Broke = _nic.AmIBroke(sum);
    }
    
    public void UseShowWhatYouBuyEvent(bool show)
    {
        Debug.Log(_countryKey);
        transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(show ? 1f : 0f, 0.5f).SetEase(Ease.InOutSine);
        if (show)
        {
            if (_countryKey == null)
            {
                return;
            }
            
            if (_countryKey.Equals(""))
            {
                transform.GetChild(1).GetComponent<TMP_Text>().text = "";
            }
            else
            {
                Utility.GetTranslatedText(_countryKey, s => transform.GetChild(1).GetComponent<TMP_Text>().text = s.ToUpper());    
            }
        }
        else
        {
            transform.GetChild(1).GetComponent<TMP_Text>().text = "";
        }
        

        transform.parent.GetComponent<Image>().DOColor(Settings.ColorMap[Tailwind.Blue1], 0.5f);
    }
}