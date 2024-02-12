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
    }

    private void ChangeModelAndLoadScene()
    {
        _jumpScene = GameState.Instance.arAvailable? "AR-Szene" : "3D-Viewer";
        Debug.Log("GO TO SCENE = "+_jumpScene);
        GameState.Instance.SetVariableAndSave(ref GameState.Instance.currentGameState.current3dModel, model);
        SceneManager.LoadScene(_jumpScene);
    }
}
