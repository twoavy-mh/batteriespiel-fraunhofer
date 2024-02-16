using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Events;
using Helpers;
using Minigame1;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateButton : MonoBehaviour, BrokerBuyEvent.IUseBrokerBuy
{

    public Image outer;
    public Image check;
    public Image plus;
    public TMP_Text amount;
    public TMP_Text type;
    public Tailwind backgroundColorWhenBought;

    public int needs;
    public string dictKey;
    private int _bought = 0;

    public bool Finished
    {
        get => _bought >= needs;
    }
    
    void Start()
    {
        SceneController.Instance.brokerBuyEvent.AddListener(UseBrokerBuyEvent);
        try
        {
            amount.text = $"{_bought.ToString().PadLeft(2, '0')}/{needs}";
        }
        catch (Exception)
        {
            Debug.Log("");
        }
        
    }

    public void UseBrokerBuyEvent(Dictionary<string, int> boughtAmount)
    {
        int relevantBought = boughtAmount[dictKey];
        _bought += relevantBought;
        try
        {
            amount.text = $"{_bought.ToString().PadLeft(2, '0')}/{needs}";
            check.gameObject.SetActive(_bought >= needs);
            if (relevantBought > 0)
            {
                StartCoroutine(ChangeColor());
            }
        }
        catch (Exception)
        {
            Debug.Log("not there");
        }
    }
    
    private IEnumerator ChangeColor()
    {
        amount.DOColor(Settings.ColorMap[Tailwind.BlueUI], 0.5f);
        outer.DOColor(Settings.ColorMap[backgroundColorWhenBought], 0.5f);
        type.DOColor(Settings.ColorMap[Tailwind.BlueUI], 0.5f);
        yield return new WaitForSeconds(3);
        outer.DOColor(Settings.ColorMap[Tailwind.BlueUI], 0.5f);
        amount.DOColor(Settings.ColorMap[backgroundColorWhenBought], 0.5f);
        type.DOColor(Settings.ColorMap[backgroundColorWhenBought], 0.5f);
    }
    
    public int GetBought() => _bought;
}
