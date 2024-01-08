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
    
    public async void SetStatus(bool a_Status)
    {
        Debug.Log(_index);
        
        Image buttonImage = GameObject.Find("ButtonImage" + _index).GetComponent<Image>();
        
        if (a_Status)
        {
            Debug.Log("Set active btn id " + _index);
            Debug.Log("Images/UI/game" + _index + "-active");
            
            buttonImage.sprite = Resources.Load("Images/UI/game" + _index + "-active", typeof(Sprite)) as Sprite;
        }
        else
        {
            buttonImage.sprite = Resources.Load<Sprite>("Images/UI/game" + _index + "-inactive");
        }
    }
    
}
