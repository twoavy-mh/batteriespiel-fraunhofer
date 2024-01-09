using System.Collections;
using System.Collections.Generic;
using Events;
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
        SetListeners();
    }

    private void SetListeners()
    {
        CollectableDelegate c = callback =>
        {
            _score += callback.value;
            _scoreText.text = _score.ToString().PadLeft(5, '0');
        };
        DataStore.Instance.collectablesScore[Collectable.Lithium].AddListener(c);
        DataStore.Instance.collectablesScore[Collectable.BlueLightning].AddListener(c);
        DataStore.Instance.collectablesScore[Collectable.YellowLightning].AddListener(c);
    }
}
