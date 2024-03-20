using System;
using Helpers;
using UnityEngine;

public class GameManagerChecker : MonoBehaviour
{
    private void Start()
    {
        if (GameObject.Find("GameManager") == null)
        {
            Debug.Log("Instantiating new Gamemanager since none was loaded in the previous scenes");
            GameObject gameManger = new GameObject("GameManager");
            gameManger.transform.SetSiblingIndex(0);
            gameManger.AddComponent<Api>();
            gameManger.AddComponent<GameManager>();
        }
    }
}