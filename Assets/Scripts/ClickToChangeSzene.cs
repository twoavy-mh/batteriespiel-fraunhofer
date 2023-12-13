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

    void Awake () {
       GetComponent<Button>().onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        Debug.Log("SZENE = " + jumpScene);
        SceneManager.LoadScene(jumpScene);
    }
}
