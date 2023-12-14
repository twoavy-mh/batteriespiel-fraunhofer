using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class LifeBar : MonoBehaviour, RegenerateEvent.IUseRegeneration
{
    private RectTransform _rt;
    private bool _isDecaying = true;
    private float _regenerationMultiplier = 1f;
    
    private Coroutine _regenerationCoroutine;
    
    void Awake()
    {
        _rt = GetComponent<RectTransform>();
        SceneController.Instance.regenerateEvent.AddListener(UseRegeneration);
    }

    // Update is called once per frame
    void Update()
    {
        _rt.sizeDelta = new Vector2(_rt.sizeDelta.x + (_isDecaying ? Time.deltaTime * -1 : Time.deltaTime * _regenerationMultiplier), _rt.sizeDelta.y);    
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
        yield return new WaitForSeconds(duration);
        _isDecaying = true;
    }

    public void UseRegeneration(RegenerationInstance settings)
    {
        Debug.Log("Regenerating");
        Regenerate(settings);
    }
}
