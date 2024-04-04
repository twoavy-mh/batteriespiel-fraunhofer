using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Helpers;
using JumpNRun;
using Models;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour, CollectedEvent.IUseCollectable
{
    private TMP_Text _scoreText;
    private int _score = 0;
    
    void Start()
    {
        SceneController.Instance.collectEvent.AddListener(UseCollectable);
        _scoreText = GetComponent<TMP_Text>();
        _scoreText.text = _score.ToString().PadLeft(5, '0');
        if ((int)GameState.Instance.GetCurrentMicrogame() == 4)
        {
            GetComponent<FontStyler>().fontColor = Tailwind.Blue2;
        }
    }

    public int GetScoreForApi()
    {
        return _score;
    }

    public void UseCollectable(Collectable c)
    {
        int newScore = 0;
        switch (c)
        {
            case Collectable.LevelSpecific:
                newScore = _score + 1000;
                break;
            case Collectable.BlueLightning:
                newScore = _score + 500;
                break;
            case Collectable.YellowLightning:
                newScore = _score + 100;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(c), c, null);
        }

        int startScore = _score;
        _score = newScore;
        StartCoroutine(Utility.AnimateAnything(0.5f, startScore, newScore,
            (progress, start, end) =>
            {
                int rounded = Mathf.RoundToInt(Mathf.Lerp(start, end, progress));
                _scoreText.text = rounded.ToString().PadLeft(5, '0');
            }));
    }
}
