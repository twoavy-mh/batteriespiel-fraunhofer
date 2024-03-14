using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame1
{
    public class CountCircleController : MonoBehaviour
    {
        public TMP_Text amount;
        public Image outerCircle;
        
        private int _count;
        private Tailwind _tailwind;
        
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                amount.text = _count.ToString();
            }
        }

        public Tailwind Color
        {
            get => _tailwind;
            set
            {
                _tailwind = value;
                amount.color = Settings.ColorMap[_tailwind];
                outerCircle.color = Settings.ColorMap[_tailwind];
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {
            Count = _count;
            Color = _tailwind;
        }

        public void SetColor(Tailwind color)
        {
            Color = color;
        }
        
        public void SetCount(int count)
        {
            Count = count;
        }
    }   
}
