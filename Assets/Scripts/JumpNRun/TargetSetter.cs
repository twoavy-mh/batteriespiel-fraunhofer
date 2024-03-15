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
            if (cg == 6)
            {
                cg = 0;
            }
            GetComponent<SpriteRenderer>().sprite = _targets[cg];
            tag = "Target";
        }
    }   
}
