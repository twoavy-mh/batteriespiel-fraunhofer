using System.Collections;
using System.Collections.Generic;
using Minigame1;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{

    public Dictionary<string, int> buyAmount = new Dictionary<string, int>()
    {
        {"nickle", 3},
        {"lithium", 5},
        {"cobalt", 3}
    };
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Buy);
    }
    
    public void SetNewBuyAmount(Dictionary<string, int> newAmount, string countryKey)
    {
        buyAmount = newAmount;
        transform.GetChild(0).GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString(countryKey).ToUpper();

    }
    

    private void Buy()
    {
        SceneController.Instance.brokerBuyEvent.Invoke(buyAmount);
    }
}
