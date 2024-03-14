using System;
using Helpers;
using Minigame3;
using UnityEngine;

public class NanoGame7MaskController : MonoBehaviour
{
    public enum FillDirection
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop,
    }
    
    public event Action OnFilled;
    
    public NanoGame7ButtonController buttonFill;

    public FillDirection fillDirection;
    
    private RectTransform _mask;
    private RectTransform _maskFill;

    private bool _isFilled = false;
    private Vector3 _initialFillPosition;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        _mask = transform.GetComponentsInChildren<RectTransform>()[0];
        _maskFill = transform.GetComponentsInChildren<RectTransform>()[1];
        _initialFillPosition = _maskFill.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isFilled)
            return;
        
        if (buttonFill.ButtonPressed() && !IsFillPositionValid())
        {
            ShiftMaskFill();
        } 
        else if (IsFillPositionValid())
        {
            _isFilled = true;
            buttonFill.DisableButton();    
            _maskFill.GetComponent<ImageColorSetter>().UpdateColor(Tailwind.Yellow3);
            OnFilled?.Invoke();
        }
    }

    private void ShiftMaskFill()
    {
        if (fillDirection == FillDirection.LeftToRight)
        {
            _maskFill.localPosition = new Vector3(_maskFill.localPosition.x + 0.75f, _maskFill.localPosition.y,
                _maskFill.localPosition.z);
        }
        else if (fillDirection == FillDirection.RightToLeft)
        {
            _maskFill.localPosition = new Vector3(_maskFill.localPosition.x - 0.75f, _maskFill.localPosition.y,
                _maskFill.localPosition.z);
        }
        else if (fillDirection == FillDirection.TopToBottom)
        {
            _maskFill.localPosition = new Vector3(_maskFill.localPosition.x, _maskFill.localPosition.y - 0.75f,
                _maskFill.localPosition.z);
        }
        else if (fillDirection == FillDirection.BottomToTop)
        {
            _maskFill.localPosition = new Vector3(_maskFill.localPosition.x, _maskFill.localPosition.y + 0.75f,
                _maskFill.localPosition.z);
        }
    }

    private bool IsFillPositionValid()
    {
        if (fillDirection == FillDirection.LeftToRight)
        {
            return _maskFill.localPosition.x >= _mask.rect.width;
        } 
        
        if (fillDirection == FillDirection.RightToLeft)
        {
            return _maskFill.localPosition.x <= -_mask.rect.width;
        }  
        
        if (fillDirection == FillDirection.TopToBottom)
        {
            return _maskFill.localPosition.y <= -_mask.rect.height/2;
        } 
        
        if (fillDirection == FillDirection.BottomToTop)
        {
            return _maskFill.localPosition.y >= _mask.rect.height/2;
        }

        return false;
    }

    public bool IsFilled()
    {
        return _isFilled;
    }

    private void OnDisable()
    {
        _maskFill.localPosition = _initialFillPosition;
        _maskFill.GetComponent<ImageColorSetter>().UpdateColor(Tailwind.Yellow2);
        _isFilled = false;
    }
}
