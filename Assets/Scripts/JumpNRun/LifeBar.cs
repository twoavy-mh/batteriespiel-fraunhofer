using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Helpers;
using Models;
using UnityEngine;
using JumpNRun;
using UnityEngine.Video;

public class LifeBar : MonoBehaviour, CollectedEvent.IUseCollectable
{
    [Range(0.0f, 1.0f)] public float startWithHealthPercent = 1.0f;

    private RectTransform _rt;
    private bool _isDecaying = true;
    private bool _isBoosting = false;

    private float _multiplier = 1f;
    
    public float maxHealth = 330f;

    private Coroutine _regenerationCoroutine;
    private Coroutine _decayCoroutine;

    private float _health = 1f;

    public float Health

    {
        get => _health;
        set
        {
            _health += (value - _health) * _multiplier;
            if (_rt)
            {
                _rt.sizeDelta = new Vector2(_health.MapBetween(0f, maxHealth, 0, 330f), _rt.sizeDelta.y);
            }
        }
    }

    void Awake()
    {
        _health = maxHealth * startWithHealthPercent;
        _rt = GetComponent<RectTransform>();
        _rt.sizeDelta = new Vector2(_rt.sizeDelta.x * startWithHealthPercent, _rt.sizeDelta.y);
        SceneController.Instance.collectEvent.AddListener(UseCollectable);
        AddScoreListeners();

        InvokeRepeating(nameof(Check), 0f, 1f);
    }

    private void Check()
    {
        if (_isDecaying)
        {
            Health--;
        }
        else if (_isBoosting)
        {
            Health++;
        }
    }

    private IEnumerator Regenerate(float duration, float increaseBy)
    {
        _isBoosting = true;
        _isDecaying = false;
        _multiplier = 5f;
        yield return new WaitForSeconds(1f);
        _multiplier = 1f;
        _isDecaying = true;
        _isBoosting = false;
    }
    
    private void AddScoreListeners()
    {
        CollectableDelegate cb = callback => { Debug.Log(callback.collectable + " " + callback.count); };
        DataStore.Instance.collectablesScore[Collectable.Lithium].AddListener(cb);
        DataStore.Instance.collectablesScore[Collectable.BlueLightning].AddListener(cb);
        DataStore.Instance.collectablesScore[Collectable.YellowLightning].AddListener(cb);
    }

    public void UseCollectable(Collectable c)
    {
        float duration = 0, increaseBy = 0;
        switch (c)
        {
            case Collectable.Lithium:
                duration = 2f;
                increaseBy = 25f;
                break;
            case Collectable.BlueLightning:
                duration = 2f;
                increaseBy = 50f;
                break;
            case Collectable.YellowLightning:
                duration = 2f;
                increaseBy = 75f;
                break;
        }

        StartCoroutine(Regenerate(duration, increaseBy));
    }
}