using Events;
using UnityEngine;
using JumpNRun;
using UnityEngine.UI;
using DG.Tweening;
using Helpers;

public class LifeBar : MonoBehaviour, CollectedEvent.IUseCollectable, DieEvent.IUseDie
{
    [Range(0.0f, 1.0f)] public float startWithHealthPercent = 1.0f;

    private RectTransform _rt;
    private bool _dead = false;
    private bool _gameStarted = false;
    
    public float maxHealth = 100f;
    public float efficiency = 1f;
    public RectTransform reference;
    public Image borderImage;
    private Image _bar;

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

    private Animator _animator;

    public float Health

    {
        get => _health;
        set
        {
            if (!_gameStarted) return;
            Debug.Log("Health set to " + value); 
            _health = value;
            if (_health <= 0)
            {
                _health = 0;
                SceneController.Instance.dieEvent.Invoke();
            }

            SetColor(GetColor());

            if (_rt && _gameStarted)
            {
                _rt.DOSizeDelta(
                    new Vector2(_health.MapBetween(0f, maxHealth * efficiency, 0, reference.sizeDelta.x * efficiency),
                        _rt.sizeDelta.y), 1f).SetEase(Ease.Linear);
            }
        }
    }

    private void Awake()
    {
        _animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Start()
    {
        Debug.Log("Lifebar mounted");

        _health = maxHealth * startWithHealthPercent;
        _bar = GetComponent<Image>();
        SetColor(GetColor(), false);
        _rt = GetComponent<RectTransform>();
        _rt.sizeDelta = new Vector2(_rt.sizeDelta.x * startWithHealthPercent, _rt.sizeDelta.y);
        SceneController.Instance.collectEvent.AddListener(UseCollectable);
        SceneController.Instance.dieEvent.AddListener(UseDie);

        InvokeRepeating(nameof(Check), 0f, 0.5f);
    }

    public void StartGame()
    {
        _gameStarted = true;
        GameObject.Find("Player").GetComponent<PlayerController>().StartRunning();
    }
    
    private void Check()
    {
        //Debug.Log(Health);
        if (_dead) return;
        Health -= 1;
    }

    private void Regenerate(float increaseBy)
    {
        Health += increaseBy;
        if (Health > maxHealth * efficiency)
        {
            Health = maxHealth * efficiency;
        }
    }

    public void UseCollectable(Collectable c)
    {
        float increaseBy = 0;
        switch (c)
        {
            case Collectable.BlueLightning:
                efficiency -= 0.1f;
                increaseBy = 6f;
                break;
            case Collectable.YellowLightning:
                efficiency -= 0.01f;
                increaseBy = 3f;
                break;
            case Collectable.LevelSpecific:
                increaseBy = 10f;
                break;
        }

        Regenerate(increaseBy);
    }

    private Color GetColor()
    {
        Color c;
        if (Health > maxHealth * 0.7f)
        {
            c = Settings.ColorMap[Tailwind.Green3];
            _healthState = HealthState.High;
        }
        else if (Health < maxHealth * 0.7f && Health > maxHealth * 0.2f)
        {
            c = Settings.ColorMap[Tailwind.Orange3];
            _healthState = HealthState.Mid;
        }
        else
        {
            c = Settings.ColorMap[Tailwind.Red1];
            _healthState = HealthState.Low;
        }

        return c;
    }

    private void SetColor(Color c, bool doTween = true)
    {
        if (_healthState == _lastHealthState) return;
        _bar.DOColor(c, doTween ? 0.5f : 0f);
        borderImage.DOColor(c, doTween ? 0.5f : 0f);
        _lastHealthState = _healthState;
        _animator.SetInteger("BatteryState", (int)_healthState);
    }
    
    public void UseDie()
    {
        _dead = true;
    }
}