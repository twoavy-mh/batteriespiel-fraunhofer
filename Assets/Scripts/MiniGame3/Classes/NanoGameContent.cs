using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEditor;
 
namespace Minigame3.Classes
{
    [Serializable]
    public class NanoGameContent
    {
        public string titleKey;
        public string bodyKey;
        public string playButtonLabelKey;
        public GameObject nanoGameObject;
        public Sprite nanoImage;
        public bool solved = false;
        
        public NanoGameContent(string titleKey, string bodyKey, string playButtonLabelKey, GameObject nanoGameObject, Sprite nanoImage)
        {
            this.titleKey = titleKey;
            this.bodyKey = bodyKey;
            this.playButtonLabelKey = playButtonLabelKey;
            this.nanoGameObject = nanoGameObject;
            this.nanoImage = nanoImage;
        }
    }
}