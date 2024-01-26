using System;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FontStyles = Helpers.FontStyles;
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
    private FontStyles _fontDetails;
    public FontStyles fontDetails {
        get {
            return _fontDetails;
        } set {
            _fontDetails = value;
            SetFont();
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
        RerenderLayout();
    } 
    private void Init(){
        fontColor = _fontColor;
        fontColorAlpha = _fontColorAlpha;
        glowColor = _glowColor;
        fontDetails = _fontDetails;
        glowSizeOutwards = _glowSizeOutwards;
        
        if (borderSize > 0)
        {
            _text.outlineWidth = borderSize;
            _text.outlineColor = Settings.ColorMap[borderColor];
        }
    }
    
    private void SetFont()
    {
        string path = $"Fonts/Roboto_{Settings.FontMap[fontDetails].fontType}/Roboto{Settings.FontMap[fontDetails].fontType}-{WeightToString(Settings.FontMap[fontDetails].fontWeight)}";
        _text.font = Resources.Load(path, typeof(TMP_FontAsset)) as TMP_FontAsset;
        _text.fontSize = Settings.FontMap[fontDetails].GetFontSizeByScreen();
        RerenderLayout();
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
        RerenderLayout();
    }

    private string WeightToString(FontWeight f)
    {
        switch (f)
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

    private void RerenderLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

}