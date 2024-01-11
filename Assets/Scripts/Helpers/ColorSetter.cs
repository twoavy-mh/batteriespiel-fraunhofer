using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class ColorSetter : MonoBehaviour
{
    [SerializeField]
    private Tailwind _imageColor;
    public Tailwind imageColor {
        get {
            return _imageColor;
        } set {
            _imageColor = value;
            _image.color = Settings.ColorMap[_imageColor];
        }
    }
    
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        Init();
    } 
    private void Init(){
        imageColor = _imageColor;
    }
}