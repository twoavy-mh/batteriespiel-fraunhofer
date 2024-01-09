using System;
using System.Threading.Tasks;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FontWeight = Helpers.FontWeight;

public class FontStyler : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float fontColorAlpha = 1.0f;
    public Tailwind fontColor;
    public FontType fontType;
    public FontWeight fontWeight;
    public int fontSize;
    public Tailwind borderColor;
    public int borderSize;
    public Tailwind glowColor;
    public float glowSizeOutwards;
    
    private void Awake()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        text.color = Settings.ColorMap[fontColor];
        text.alpha = fontColorAlpha;
        string path = $"Fonts/Roboto_{fontType}/Roboto{fontType}-{WeightToString()}";
        text.font = Resources.Load(path, typeof(TMP_FontAsset)) as TMP_FontAsset;
        text.fontSize = fontSize;
        if (glowSizeOutwards > 0)
        {
            text.fontSharedMaterial.EnableKeyword("GLOW_ON");
            text.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, Settings.ColorMap[glowColor]);
            text.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowSizeOutwards);    
        }

        if (borderSize > 0)
        {
            text.outlineWidth = borderSize;
            text.outlineColor = Settings.ColorMap[borderColor];
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