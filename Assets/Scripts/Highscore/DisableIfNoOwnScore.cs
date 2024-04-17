using System.Collections;
using System.Collections.Generic;
using Helpers;
using Highscore;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableIfNoOwnScore : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (SceneManager.GetActiveScene().name == "Onboarding")
        {
            gameObject.SetActive(false);
            yield break;
        }
        yield return new WaitUntil(() => GameState.Instance != null);
        if (GameState.Instance.currentGameState.results.Length == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
