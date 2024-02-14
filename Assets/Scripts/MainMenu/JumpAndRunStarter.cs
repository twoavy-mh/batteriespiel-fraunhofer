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
            GetComponent<Button>().onClick.AddListener(StartIfStartable);
        }

        private void StartIfStartable()
        {
            if (GameState.Instance.currentGameState.results.Length == 0)
            {
                SceneManager.LoadScene("JumpNRun");
                return;
            }
            if (GameState.Instance.currentGameState.results.Last().finished)
            {
                SceneManager.LoadScene("JumpNRun");
            }
            else
            {
                Debug.Log($"Now do minigame {GameState.Instance.currentGameState.results.Length - 1}");
            }
        }
        
    }
}