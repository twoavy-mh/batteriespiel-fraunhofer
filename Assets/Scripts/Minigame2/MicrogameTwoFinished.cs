using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MicrogameTwoFinished : MonoBehaviour
{
    private int score;
    public GameObject again;
    public GameObject next;

    private void Start()
    {
        again.GetComponent<Button>().onClick.AddListener(Retry);
        next.GetComponent<Button>().onClick.AddListener(Next);
        
        again.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("minigame2Again");
        next.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("next");
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
        transform.GetChild(3).GetComponent<TMP_Text>().text = LocalizationSettings.StringDatabase
            .GetLocalizedString("minigame2Score")
            .Replace("~", score.ToString())
            .Replace("#", totalTries.ToString());
    }
}
