using System;
using Helpers;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class MultilangImage
    {
        public Sprite De;
        public Sprite En;

        public Sprite GetSprite()
        {
            switch (GameState.Instance.currentGameState.language)
            {
                case Language.De:
                    if (De != null) return De;
                    return En;
                case Language.En:
                    if (En != null) return En;
                    return De;
                default:
                    return De == null ? En : De;
            }
        }
    }
}