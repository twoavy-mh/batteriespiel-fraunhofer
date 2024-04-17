using System;
using System.Collections.Generic;
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
            int totalTime = (int)Math.Floor((DateTime.Now - SceneController.Instance.startTime).TotalSeconds);
            int minutes = totalTime / 60;
            int seconds = totalTime % 60;
            
            int innerScore = Math.Max(Math.Max(0, 100 - (fails * 5)) - (6 - (totalTries - fails)) * 17, 0);
            _score = innerScore;
            Utility.GetTranslatedText(innerScore > 60 ? "microgame_2_did_good" : "microgame_2_did_bad", s =>
                transform.GetChild(4).GetComponent<TMP_Text>().text = s, new Dictionary<string, string>()
            {
                { "~n", totalTries - fails + "" },
                { "~p", innerScore + "" },
                { "~m", (minutes + "").PadLeft(2, '0') },
                { "~h", (seconds + "").PadLeft(2, '0') }
            });
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