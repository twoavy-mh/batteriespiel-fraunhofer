using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class LifeBar : MonoBehaviour, RegenerateEvent.IUseRegeneration, DecayEvent.IUseDecay
{
    private RectTransform _rt;
    private bool _isDecaying = true;
    private bool _isRegenerating = false;
    private bool _isBoosting = false;
    private float _regenerationMultiplier = 1f;
    private float _decayMultiplier = 1f;
    
    private Coroutine _regenerationCoroutine;
    
    void Awake()
    {
        _rt = GetComponent<RectTransform>();
        SceneController.Instance.regenerateEvent.AddListener(UseRegeneration);
        SceneController.Instance.decayEvent.AddListener(UseDecay);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDecaying)
        {
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x + Time.deltaTime * -2, _rt.sizeDelta.y);
        } else if (_isRegenerating)
        {
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x + Time.deltaTime * _regenerationMultiplier, _rt.sizeDelta.y);    
        } else if (_isBoosting)
        {
            _rt.sizeDelta = new Vector2(_rt.sizeDelta.x + Time.deltaTime * _decayMultiplier, _rt.sizeDelta.y);
        }
        
    }
    
    public void Regenerate(RegenerationInstance settings)
    {
        _regenerationMultiplier = settings.multiplier;
        if (!_isDecaying)
        {
            _isDecaying = false;
            if (_regenerationCoroutine != null)
            {
                StopCoroutine(_regenerationCoroutine);
            }
            _regenerationCoroutine = StartCoroutine(WaitForRegeneration(settings.duration));
        }
        else
        {
            _isDecaying = false;
            _regenerationCoroutine = StartCoroutine(WaitForRegeneration(settings.duration));
        }
    }

    private IEnumerator WaitForRegeneration(float duration)
    {
        _isRegenerating = true;
        yield return new WaitForSeconds(duration);
        _isDecaying = true;
        _isRegenerating = false;
    }

    public void UseRegeneration(RegenerationInstance settings)
    {
        Debug.Log("Regenerating");
        Regenerate(settings);
    }
    
    private IEnumerator WaitForDecay(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
    
    public void UseDecay(DecayInstance settings)
    {
        _isDecaying = false;
        _decayMultiplier = settings.multiplier;
        _isBoosting = true;
        StartCoroutine(WaitForDecay(settings.duration));
        _isBoosting = false;
        _isDecaying = true;
    }
}
