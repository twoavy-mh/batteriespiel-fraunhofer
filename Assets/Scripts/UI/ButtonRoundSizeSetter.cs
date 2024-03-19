using UnityEngine;
using Helpers;
using Nobi.UiRoundedCorners;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ButtonRoundSizeSetter : MonoBehaviour
    {
        [FormerlySerializedAs("iconSizeDelta")] public Vector2 iconSizeDeltaMobileFigma = new Vector2(5f, 15f);

        void Start()
        {
            float multiplier = Settings.RESIZE_FACTOR;

            /**
             * Get all components *
             */
            RectTransform outlineRt = GetComponent<RectTransform>();
            ImageWithRoundedCorners outlineIwrc = GetComponent<ImageWithRoundedCorners>();

            ImageWithRoundedCorners backgroundIwrc = transform.GetChild(0).GetComponent<ImageWithRoundedCorners>();

            RectTransform iconRt = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

            /**
             * SET SIZES *
             */
            Vector2 sizeDelta;
            if (Utility.GetDevice() == Device.Mobile)
            {
                sizeDelta = new Vector2(28 * multiplier, 28 * multiplier);
                outlineIwrc.radius = 14 * multiplier;
                backgroundIwrc.radius = 14 * multiplier;
                iconRt.sizeDelta = new Vector2(iconSizeDeltaMobileFigma.x * multiplier, iconSizeDeltaMobileFigma.y * multiplier);
            }
            else
            {
                sizeDelta = new Vector2(38, 38);
            }

            outlineRt.sizeDelta = sizeDelta;

            /**
             * RESET ALL LAYOUTS *
             */
            LayoutRebuilder.ForceRebuildLayoutImmediate(outlineRt);
        }
    }
}
