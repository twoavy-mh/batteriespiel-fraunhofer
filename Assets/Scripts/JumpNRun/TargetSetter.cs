using System;
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
            int cg = Math.Min((int)GameState.Instance.GetCurrentMicrogame(), 5);
            if (cg == 5)
            {
                cg = new System.Random().Next(0, targets.Length - 1);
            }
            GetComponent<SpriteRenderer>().sprite = targets[cg];
            tag = "Target";
        }
    }   
}
