using Minigame1;
using TMPro;
using UnityEngine;

public class NonInvestedController : MonoBehaviour
{
    public SceneController sceneController;
    private int _currentMoney;

    public int currentMoney
    {
        get
        {
            return _currentMoney; 
        }
        private set
        {
            _currentMoney = value;
        }
    }

    private void Awake()
    {
        currentMoney = sceneController.startCapital;
        GetComponent<TMP_Text>().text = currentMoney + "€";
    }

    public void Spend(int amount)
    {
        currentMoney -= amount;
        GetComponent<TMP_Text>().text = currentMoney + "€";
        
        SceneController.Instance.moneySpentEvent.Invoke(_currentMoney);
    }

    public bool AmIBroke(int wantsToSpend)
    {
        return _currentMoney - wantsToSpend < 0;
    }
}
