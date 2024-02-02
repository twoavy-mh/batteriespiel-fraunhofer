using System;
using System.Collections;
using System.Collections.Generic;
using Events;
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

    public int needs;
    public string dictKey;
    private int _bought = 0;
    
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseBrokerBuyEvent(Dictionary<string, int> boughtAmount)
    {
        int relevantBought = boughtAmount[dictKey];
        _bought += relevantBought;
        try
        {
            amount.text = $"{_bought.ToString().PadLeft(2, '0')}/{needs}";
        }
        catch (Exception)
        {
            Debug.Log("not there");
        }
        
    }
}
