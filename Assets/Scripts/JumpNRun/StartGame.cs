using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGameCb);
    }
    
    void StartGameCb()
    {
        GameObject.Find("Canvas").GetComponentInChildren<LifeBar>().StartGame();
        GameObject.Find("StartScreen").SetActive(false);
    }

}
