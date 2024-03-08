using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class MicrogameFinishedEvent : UnityEvent<GameState.Microgames>
    {
        public interface IUseMicrogameFinished
        {
            public void UseMicrogameFinishedEvent(GameState.Microgames microgame);
        }
    }
}