using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Extensions
{
        public class ImageBasedAspectRatioFitter : AspectRatioFitter
        {
            private Image _image;
            protected override void Start() 
            { 
                base.Start();
                UpdateRatioAndImage();
            }

            public void UpdateRatioAndImage()
            {
                //AspectRatioFitter aspectRatioFitter = GetComponent<AspectRatioFitter>();
                // aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                _image = GetComponent<Image>();
                if (_image.sprite == null) return;
                float aspectRatio = (float)_image.sprite.texture.width / _image.sprite.texture.height;
                base.aspectRatio = aspectRatio;
            }
        }
}