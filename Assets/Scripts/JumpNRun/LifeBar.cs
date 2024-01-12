using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Models;
using UnityEngine;
using JumpNRun;

public class LifeBar : MonoBehaviour, CollectedEvent.IUseCollectable, DecayEvent.IUseDecay
{
    [Range(0.0f, 1.0f)] public float startWithHealthPercent = 1.0f;

    private RectTransform _rt;
    private bool _isDecaying = true;
    private bool _isRegenerating = false;
    private bool _isBoosting = false;
    private float _regenerationMultiplier = 1f;
    private float _decayMultiplier = 1f;

    private Coroutine _regenerationCoroutine;
    private Coroutine _decayCoroutine;

    void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _rt.sizeDelta = new Vector2(_rt.sizeDelta.x * startWithHealthPercent, _rt.sizeDelta.y);
        SceneController.Instance.collectEvent.AddListener(UseCollectable);
        SceneController.Instance.decayEvent.AddListener(UseDecay);
        AddScoreListeners();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDecaying)
        {
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x + Time.deltaTime * -2, _rt.sizeDelta.y);
        }
        else if (_isBoosting)
        {
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x + (Time.deltaTime * _decayMultiplier), _rt.sizeDelta.y);
        }
    }

    public void Regenerate(float duration, float increaseBy)
    {
        _isDecaying = false;
        StartCoroutine(Helpers.Utility.AnimateAnything(duration, _rt.sizeDelta.x, _rt.sizeDelta.x + increaseBy,
            (progress, start, end) => _rt.sizeDelta = new Vector2(Mathf.Lerp(start, end, progress), _rt.sizeDelta.y),
            () => _isDecaying = true));
    }

    private IEnumerator WaitForDecay(DecayInstance settings, Action callback)
    {
        _isDecaying = false;
        _decayMultiplier = settings.multiplier;
        _isBoosting = true;
        yield return new WaitForSeconds(settings.duration);
        _isBoosting = false;
        _isDecaying = true;
        callback.Invoke();
    }

    public void UseDecay(DecayInstance settings)
    {
        _decayCoroutine = StartCoroutine(WaitForDecay(settings, () => _decayCoroutine = null));
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

        Regenerate(duration, increaseBy);
    }
}