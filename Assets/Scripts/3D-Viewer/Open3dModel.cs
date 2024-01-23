using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Open3dModel : MonoBehaviour
{
    [SerializeField]
    public GameState.Models model;

    private String _jumpScene;
    void Awake () {
       GetComponent<Button>().onClick.AddListener(ChangeModelAndLoadScene);
       Debug.Log(GameState.Instance.arAvailable);
       _jumpScene = GameState.Instance.arAvailable? "AR-Szene" : "3D-Viewer";
    }

    private void ChangeModelAndLoadScene()
    {
        GameState.Instance.SetVariableAndSave(ref GameState.Instance.current3dModel, model);
        SceneManager.LoadScene(_jumpScene);
    }
}
