using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressRingController : MonoBehaviour
    {

        public Image fillingRing;
        public TMP_Text progressText;
        public FontStyler fontStyler;
        
        public void StartAnimation(float scored)
        {
            StartCoroutine(Helpers.Utility.AnimateAnything(1f, 0f, scored, ((progress, start, end) =>
            {
                fillingRing.fillAmount = Mathf.Lerp(start, end, progress) / 100f;
                progressText.text = Mathf.RoundToInt(Mathf.Lerp(start, end, progress)).ToString();
            })));
        }
    }
}
