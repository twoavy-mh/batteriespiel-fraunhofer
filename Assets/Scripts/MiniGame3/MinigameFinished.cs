using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Minigame3
{
    public class MinigameFinished : MonoBehaviour
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

        public void SetScore(int solvedCount, int sumCount)
        {
            int score = (int) Math.Round(solvedCount * ( 100 / (float) sumCount));
            
            Utility.GetTranslatedText(score > 60? "mg3_endscreen_good":"mg3_endscreen_well", s => scoreText.text = s);
            
            Utility.GetTranslatedText("points_reached", s => scoreText.text += s
                .Replace("~s", score.ToString()));
            pgc.StartAnimation(score);
        }
    }
   
}