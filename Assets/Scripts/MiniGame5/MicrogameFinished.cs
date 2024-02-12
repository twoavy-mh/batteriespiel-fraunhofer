using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Minigame5
{
    public class MicrogameFinished : MonoBehaviour
    {
        private int score;
        public GameObject again;
        public GameObject next;
        public UI.ProgressRingController pgc;

        private void Start()
        {
            again.GetComponent<Button>().onClick.AddListener(Retry);
            next.GetComponent<Button>().onClick.AddListener(Next);
        }

        private void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    
        private void Next()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void SetScore(int correctAnswered)
        {
            int score = (int) Math.Max(0, correctAnswered * 12.5f);
            transform.GetChild(3).GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase
                .GetLocalizedString("minigame5Score")
                .Replace("~", score.ToString());
            pgc.StartAnimation(score);
        }
    }
   
}