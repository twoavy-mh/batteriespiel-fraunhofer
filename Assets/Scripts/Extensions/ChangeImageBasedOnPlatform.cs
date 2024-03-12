using UnityEngine;
using UnityEngine.UI;
using Helpers;

namespace Extensions
{
        public class ChangeImageBasedOnPlatform : MonoBehaviour
        {
            public Sprite mobileSprite;
            public Sprite desktopSprite;
            
            private Image _image;
            void Start()
            {
                _image = GetComponent<Image>();
                _image.sprite = Utility.GetDevice() == Device.Mobile ? mobileSprite : desktopSprite;
                
                GetComponent<ImageBasedAspectRatioFitter>()?.UpdateRatioAndImage();
            }
    }
}