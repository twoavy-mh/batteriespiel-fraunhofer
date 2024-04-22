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
            int cg = (int)GameState.Instance.GetCurrentMicrogame();
            if (cg == 5)
            {
                cg = new System.Random().Next(0, _targets.Length - 1);
            }
            GetComponent<SpriteRenderer>().sprite = _targets[cg];
            tag = "Target";
        }
    }   
}
