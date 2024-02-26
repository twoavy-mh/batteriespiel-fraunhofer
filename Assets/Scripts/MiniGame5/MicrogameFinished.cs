using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
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
        public TMP_Text scoreText;

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

        public void SetScore(int correctAnswered, int questionCount, float completionTime)
        {
            int score = (int) Math.Round(correctAnswered * ( 100 / (float) questionCount));
            
            int minutes = (int) completionTime / 60;
            int seconds = (int) completionTime % 60;
            string time = minutes.ToString().PadLeft(2, '0') + ":" + seconds;  
            
            Utility.GetTranslatedText("minigame5Score", s => scoreText.text = s
                .Replace("~i", correctAnswered.ToString())
                .Replace("~c", questionCount.ToString())
                .Replace("~t", time)
                .Replace("~s", score.ToString()));
            pgc.StartAnimation(score);
        }
    }
   
}