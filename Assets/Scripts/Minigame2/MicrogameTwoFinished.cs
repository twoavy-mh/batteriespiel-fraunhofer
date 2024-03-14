using System;
using Helpers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Minigame2
{
    public class MicrogameTwoFinished : MonoBehaviour
    {
        private int _score;
        public GameObject again;
        public GameObject next;
        public UI.ProgressRingController pgc;

        private void Start()
        {
            again.GetComponent<Button>().onClick.AddListener(Retry);
            next.GetComponent<Button>().onClick.AddListener(Next);

            Utility.GetTranslatedText("minigame2Again",
                s => again.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = s);
            Utility.GetTranslatedText("next",
                s => next.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = s);
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
            int innerScore = Math.Max(0, 100 - (fails * 5));
            _score = innerScore;
            Utility.GetTranslatedText(innerScore > 60 ? "microgame_2_did_good" : "microgame_2_did_bad", s =>
                transform.GetChild(4).GetComponent<TMP_Text>().text = s
                    .Replace("~", innerScore.ToString())
                    .Replace("#", totalTries.ToString()));
            pgc.StartAnimation(_score);
            MicrogameState s = new MicrogameState();
            s.finished = true;
            s.unlocked = true;
            s.result = innerScore;
            s.jumpAndRunResult = GameState.Instance.currentGameState.results[1].jumpAndRunResult;
            s.game = GameState.Microgames.Microgame2;
            StartCoroutine(Api.Instance.SetGame(s, GameState.Instance.currentGameState.id,
                details => { GameState.Instance.currentGameState = details; }));
        }
    }
}