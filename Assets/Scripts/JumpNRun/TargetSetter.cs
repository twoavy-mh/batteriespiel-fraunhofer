using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace JumpNRun
{
    public class TargetSetter : MonoBehaviour
    {

        private Sprite[] _targets;
    
        void Start()
        {
            _targets = Resources.LoadAll<Sprite>("Images/JnRLevel/target");
            GetComponent<SpriteRenderer>().sprite = _targets[(int)GameState.Instance.GetCurrentMicrogame()];
            tag = "Target";
        }
    }   
}
