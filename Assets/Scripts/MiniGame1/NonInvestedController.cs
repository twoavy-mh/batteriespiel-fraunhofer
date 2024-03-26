using Minigame1;
using TMPro;
using UnityEngine;

public class NonInvestedController : MonoBehaviour
{
    public SceneController sceneController;
    private int _currentMoney;

    private void Awake()
    {
        _currentMoney = sceneController.startCapital;
        GetComponent<TMP_Text>().text = _currentMoney + "€";
    }

    public void Spend(int amount)
    {
        _currentMoney -= amount;
        GetComponent<TMP_Text>().text = _currentMoney + "€";
        
        SceneController.Instance.moneySpentEvent.Invoke(_currentMoney);
    }

    public bool AmIBroke(int wantsToSpend)
    {
        return _currentMoney - wantsToSpend < 0;
    }
}
