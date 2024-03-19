using LeTai.TrueShadow;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
        public class ShadowSetter : TrueShadow
        {
            public enum ShadowType
            {
                Dark, Yellow, Blue
            }
            
            [SerializeField]
            private ShadowType _shadowType;
            
            public ShadowType shadowType
            {
                get => _shadowType;
                set
                {
                    _shadowType = value;
                    SetShadowType();
                }
            }
            
            protected override void Start() 
            { 
                base.Start(); 
                Size = 14;
                OffsetDistance = 0;
                SetShadowType();
            }

            public void SetShadowType()
            {
                switch (_shadowType)
                {
                    case ShadowType.Dark:
                        Color = new Color(0, 0, 0, 0.5f);
                        break;
                    case ShadowType.Yellow:
                        Color = new Color(1, 1, 0, 0.5f);
                        break;
                    case ShadowType.Blue:
                        Color = new Color(0, 0, 1, 0.5f);
                        break;
                }
            }
        }
}