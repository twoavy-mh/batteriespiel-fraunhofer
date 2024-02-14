using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Helpers;
using Models;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private TMP_Text _scoreText;
    private int _score = 0;
    
    void Start()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (GameState.Instance.currentGameState == null)
        {
            return;
        }
        if (_score != GameState.Instance.currentGameState.totalScore)
        {
            StartCoroutine(Utility.AnimateAnything(0.5f, _score, GameState.Instance.currentGameState.totalScore,
                (progress, start, end) =>
                {
                    _score = Mathf.RoundToInt(Mathf.Lerp(start, end, progress));
                    _scoreText.text = _score.ToString().PadLeft(5, '0');
                }));
        }
    }

    public int GetScoreForApi()
    {
        return _score;
    }
}
