using System;
using Helpers;
using TMPro;
using UnityEngine;
using FontWeight = Helpers.FontWeight;

public class FontStyler : MonoBehaviour
{
    [Range(0f, 1f), SerializeField]
    private float _fontColorAlpha = 1f;
    
    public float fontColorAlpha 
    {
        get {
            return _fontColorAlpha;
        } set {
            _fontColorAlpha = value;
            _text.alpha = _fontColorAlpha;
        }
    }
    
    [SerializeField]
    private Tailwind _fontColor;
    public Tailwind fontColor {
        get {
            return _fontColor;
        } set {
            _fontColor = value;
            _text.color = Settings.ColorMap[_fontColor];
        }
    }
    
    [SerializeField]
    private FontType _fontType;
    public FontType fontType {
        get {
            return _fontType;
        } set {
            _fontType = value;
            SetFont();
        }
    }
    
    [SerializeField]
    private FontWeight _fontWeight;
    public FontWeight fontWeight {
        get {
            return _fontWeight;
        } set {
            _fontWeight = value;
            SetFont();
        }
    }
    
    [SerializeField]
    private int _fontSize;
    public int fontSize
    {
        get {
            return _fontSize;
        } set {
            _fontSize = value;
            _text.fontSize = _fontSize;
            
        }
    }
    
    [SerializeField]
    private  Tailwind _borderColor;
    public Tailwind borderColor{
        get {
            return _borderColor;
        } set {
            _borderColor = value;
            SetBorder();
        }
    }
    
    [SerializeField]
    private  int _borderSize;
    public int borderSize{
        get {
            return _borderSize;
        } set {
            _borderSize = value;
            SetBorder();
        }
    }
    
    
    [SerializeField]
    private  Tailwind _glowColor;
    public Tailwind glowColor{
        get {
            return _glowColor;
        } set {
            _glowColor = value;
            SetGlow();
        }
    }
    
    [SerializeField]
    private  float _glowSizeOutwards;
    public float glowSizeOutwards {
        get {
            return _glowSizeOutwards;
        } set {
            _glowSizeOutwards = value;
            SetGlow();
        }
    }

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        Init();
    } 
    private void Init(){
        fontColor = _fontColor;
        fontColorAlpha = _fontColorAlpha;
        fontType = _fontType;
        fontWeight = _fontWeight;
        fontSize = _fontSize;
        glowColor = _glowColor;
        glowSizeOutwards = _glowSizeOutwards;
        
        if (borderSize > 0)
        {
            _text.outlineWidth = borderSize;
            _text.outlineColor = Settings.ColorMap[borderColor];
        }
    }
    
    private void SetFont()
    {
        string path = $"Fonts/Roboto_{fontType}/Roboto{fontType}-{WeightToString()}";
        _text.font = Resources.Load(path, typeof(TMP_FontAsset)) as TMP_FontAsset;
    }

    private void SetGlow()
    {
        if (glowSizeOutwards > 0)
        {
            _text.fontSharedMaterial.EnableKeyword("GLOW_ON");
            _text.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, Settings.ColorMap[glowColor]);
            _text.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowSizeOutwards);    
        }
        else
        {
            _text.fontSharedMaterial.DisableKeyword("GLOW_ON");
        }
    }
    
    private void SetBorder()
    {
        if (borderSize > 0)
        {
            _text.outlineWidth = borderSize;
            _text.outlineColor = Settings.ColorMap[borderColor];
        }
        else
        {
            _text.outlineWidth = 0;
        }
    }

    private string WeightToString()
    {
        switch (fontWeight)
        {
            case FontWeight.Bold700:
                return "Bold";
            case FontWeight.Medium500:
                return "Medium";
            case FontWeight.Regular400:
                return "Regular";
        }

        return "Medium";
    }
    
}