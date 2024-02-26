using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
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
            Utility.GetTranslatedText("minigame2Score", s => transform.GetChild(3).GetComponent<TMP_Text>().text = s.Replace("~", score.ToString())
                .Replace("#", totalTries.ToString()));
        }
    }
    
}
