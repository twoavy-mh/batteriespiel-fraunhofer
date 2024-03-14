using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Minigame4
{
    public class MicrogameFourFinished : MonoBehaviour
    {
        private int score;
        public GameObject again;
        public GameObject next;
        public UI.ProgressRingController pgc;

        private void Start()
        {
            again.GetComponent<Button>().onClick.AddListener(Retry);
            next.GetComponent<Button>().onClick.AddListener(Next);
            Utility.GetTranslatedText("minigame2Again", s => again.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = s);
            Utility.GetTranslatedText("next", s => next.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = s);
        }

        private void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    
        private void Next()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void SetScore(int fails, int totalTries)
        {
            int score = Math.Max(0, 100 - (fails * 5));
            Utility.GetTranslatedText("minigame2Score", s => transform.GetChild(4).GetComponent<TMP_Text>().text = s
                .Replace("~", score.ToString())
                .Replace("#", totalTries.ToString()));
            pgc.StartAnimation(score);
            MicrogameState s = new MicrogameState();
            s.finished = true;
            s.unlocked = true;
            s.result = score;
            s.jumpAndRunResult = GameState.Instance.currentGameState.results[3].jumpAndRunResult;
            s.game = GameState.Microgames.Microgame4;
            StartCoroutine(Api.Instance.SetGame(s, GameState.Instance.currentGameState.id, details =>
            {
                GameState.Instance.currentGameState = details;
            }));
        }
    }
    
}
