using Minigame1;
using TMPro;
using UnityEngine;

public class NonInvestedController : MonoBehaviour
{
    public int startMoney = 1000;

    private void Awake()
    {
        GetComponent<TMP_Text>().text = startMoney + "€";
    }

    public void Spend(int amount)
    {
        startMoney -= amount;
        GetComponent<TMP_Text>().text = startMoney + "€";
        
        SceneController.Instance.moneySpentEvent.Invoke(startMoney);
    }

    public bool AmIBroke(int wantsToSpend)
    {
        return startMoney - wantsToSpend < 0;
    }
}
