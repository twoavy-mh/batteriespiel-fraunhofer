using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace JumpNRun
{
    public class TargetSetter : MonoBehaviour
    {

        public Sprite[] targets;
    
        void Start()
        {
            int cg = (int)GameState.Instance.GetCurrentMicrogame();
            if (cg == 5)
            {
                cg = new System.Random().Next(0, targets.Length - 1);
            }
            GetComponent<SpriteRenderer>().sprite = targets[cg];
            tag = "Target";
        }
    }   
}
