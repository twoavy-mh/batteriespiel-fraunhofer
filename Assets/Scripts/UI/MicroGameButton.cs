using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicroGameButton : MonoBehaviour
{
    [SerializeField] private int _index;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetIndex(int a_Index)
    {
        _index = a_Index;
    }
    
    public void SetStatus(bool a_Status)
    {
        
        Image buttonImage = GameObject.Find("ButtonImage").GetComponent<Image>();
        
        if (a_Status)
        {
            Debug.Log("Set active btn id " + _index);
            buttonImage.sprite = Resources.Load<Sprite>("Images/UI/game" + _index + "-active");
        }
        else
        {
            buttonImage.sprite = Resources.Load<Sprite>("Images/UI/game" + _index + "-inactive");
        }
    }
    
}
