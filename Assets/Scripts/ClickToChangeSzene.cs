using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickToChangeSzene : MonoBehaviour
{
    [SerializeField]
    public String jumpScene;
    
    public Boolean goBackToLastScene = false;

    void Awake () {
       GetComponent<Button>().onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        Debug.Log(GameManager.Instance.lastScene);
        if (goBackToLastScene &&  GameManager.Instance.lastScene != "")
        {
            Debug.Log("SZENE = " + GameManager.Instance.lastScene);
            SceneManager.LoadScene(GameManager.Instance.lastScene);
            return;
        }
        Debug.Log("SZENE = " + jumpScene);
        SceneManager.LoadScene(jumpScene);
    }
}
