using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Open3dModel : MonoBehaviour
{
    [SerializeField] public GameState.Models model;

    private String _jumpScene;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Api.Instance != null);
        yield return new WaitUntil(() => GameManager.Instance != null);
        yield return new WaitUntil(() => GameState.Instance.currentGameState != null);
        Debug.Log("Start Open3dModel.cs");
        GetComponent<Button>().onClick.AddListener(ChangeModelAndLoadScene);
    }

    private void ChangeModelAndLoadScene()
    {
        _jumpScene = GameState.Instance.arAvailable ? "AR-Szene" : "3D-Viewer";
        Debug.Log("GO TO SCENE = " + _jumpScene + " WITH MODELL = " + model);
        Debug.Log(GameState.Instance.currentGameState);
        GameState.Instance.SetVariableAndSave(ref GameState.Instance.currentGameState.current3dModel, model);
        SceneManager.LoadScene(_jumpScene);
    }
}