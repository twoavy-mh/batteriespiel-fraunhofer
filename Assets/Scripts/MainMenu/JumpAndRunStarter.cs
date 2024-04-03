using System;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class JumpAndRunStarter : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(GameState.Instance.currentGameState.ToString());
            GetComponent<Button>().onClick.AddListener(StartIfStartable);
        }

        private void StartIfStartable()
        {
            
            if (GameState.Instance.currentGameState.results.Length == 0)
            {
                Debug.Log("in 1?");
                SceneManager.LoadSceneAsync("JumpNRun");
                return;
            }

            
            if (GameState.Instance.currentGameState.results.Last().finished)
            {
                Debug.Log("in 2?");
                SceneManager.LoadSceneAsync("JumpNRun");
            }
            else
            {
                Debug.Log("in 3?");
                Debug.Log($"Now do minigame {GameState.Instance.currentGameState.results.Length - 1}");
            }
        }
        
    }
}