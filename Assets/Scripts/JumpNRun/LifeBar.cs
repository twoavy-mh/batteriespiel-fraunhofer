using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Models;
using UnityEngine;
using JumpNRun;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;
using Helpers;

public class LifeBar : MonoBehaviour, CollectedEvent.IUseCollectable
{
    [Range(0.0f, 1.0f)] public float startWithHealthPercent = 1.0f;

    private RectTransform _rt;
    private bool _isDecaying = true;
    private bool _isBoosting = false;

    private float _multiplier = 1f;
    
    public float maxHealth = 330f;
    public RectTransform reference;
    public Image borderImage;
    private Image _bar;

    private Coroutine _regenerationCoroutine;
    private Coroutine _decayCoroutine;

    private float _health = 1f;
    
    private enum HealthState
    {
        High,
        Mid,
        Low, 
        Start
    }

    private HealthState _healthState = HealthState.Start;
    private HealthState _lastHealthState = HealthState.Start;
    
    public float Health

    {
        get => _health;
        set
        {
            _health += (value - _health) * _multiplier;
            if (_health <= 0)
            {
                _health = 0;
                SceneController.Instance.dieEvent.Invoke();
            }
            
            SetColor(GetColor());
            
            if (_rt)
            {
                _rt.DOSizeDelta(new Vector2(_health.MapBetween(0f, maxHealth, 0, reference.sizeDelta.x), _rt.sizeDelta.y), 1f).SetEase(Ease.Linear);
            }
        }
    }

    void Start()
    {
        if (Utility.GetDevice() == Device.Mobile)
        {
            //SetToMobile();
        }

        _health = maxHealth * startWithHealthPercent;
        _bar = GetComponent<Image>();
        SetColor(GetColor(), false);
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
            Health -= 10;
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
        _multiplier = increaseBy;
        yield return new WaitForSeconds(duration);
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
                increaseBy = 5f;
                break;
            case Collectable.BlueLightning:
                duration = 2f;
                increaseBy = 5f;
                break;
            case Collectable.YellowLightning:
                duration = 2f;
                increaseBy = 5f;
                break;
        }

        StartCoroutine(Regenerate(duration, increaseBy));
    }

    private Color GetColor()
    {
        Color c;
        if (_health > maxHealth * 0.3f)
        {
            c = Settings.ColorMap[Tailwind.Green3];
            _healthState = HealthState.High;
        } else if (_health < maxHealth * 0.3f && _health > maxHealth * 0.1f)
        {
            c = Settings.ColorMap[Tailwind.Orange3];
            _healthState = HealthState.Mid;
        } else
        {
            c = Settings.ColorMap[Tailwind.Red1];
            _healthState = HealthState.Low;
        }

        return c;
    }
    
    private void SetColor(Color c, bool doTween = true)
    {
        if (_healthState != _lastHealthState)
        {
            _bar.DOColor(c, doTween ? 0.5f : 0f);
            borderImage.DOColor(c, doTween ? 0.5f : 0f);
            _lastHealthState = _healthState;
        }
    }

    private void SetToMobile()
    {
        GameObject life = GameObject.Find("Life");
        GameObject glow = GameObject.Find("Glow");
        GameObject wrapper = transform.parent.gameObject;
        GameObject bar = transform.gameObject;
        GameObject border = GameObject.Find("border");
        GameObject score = GameObject.Find("Score");
        GameObject ladezustand = GameObject.Find("Ladezustand");
        GameObject energiestatus = GameObject.Find("Energiestatus");

        //ladezustand.GetComponent<RectTransform>().localPosition = new Vector3(-1405f, 128f, 0);
        //score.GetComponent<RectTransform>().localPosition = new Vector3(-1405f, 128f, 0);
        
        life.GetComponent<RectTransform>().sizeDelta = new Vector2(228f * Settings.RESIZE_FACTOR, 78f * Settings.RESIZE_FACTOR);
        glow.GetComponent<RectTransform>().sizeDelta = new Vector2(205.4f * Settings.RESIZE_FACTOR, 55f * Settings.RESIZE_FACTOR);
        glow.GetComponent<RectTransform>().localPosition = new Vector3(114f * Settings.RESIZE_FACTOR, (-39.7f + 1.4f) * Settings.RESIZE_FACTOR, 0f);
        wrapper.GetComponent<RectTransform>().sizeDelta = new Vector2(203.4f * Settings.RESIZE_FACTOR, 53f * Settings.RESIZE_FACTOR);
        wrapper.GetComponent<RectTransform>().localPosition = new Vector3(12f * Settings.RESIZE_FACTOR, -11.5f * Settings.RESIZE_FACTOR, 0f);
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(198f * Settings.RESIZE_FACTOR, 55f * Settings.RESIZE_FACTOR);
        border.GetComponent<RectTransform>().sizeDelta = new Vector2(border.GetComponent<RectTransform>().sizeDelta.x * Settings.RESIZE_FACTOR, 55f * Settings.RESIZE_FACTOR);
    }
    
}