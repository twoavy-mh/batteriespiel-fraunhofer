using System;
using System.Threading.Tasks;
using Events;
using Models;
using UnityEngine;

namespace Helpers
{
    
    [Serializable]
    public class GameState
    {
        public static GameState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameState();
                }
                return _instance;
            }
        }
        
        private static GameState _instance;
        
        public enum Microgames
        {
            Microgame1, Microgame2, Microgame3, Microgame4, Microgame5
        }
        
        public enum Models
        {
            Cells, Pouch, Car
        }
        
        public bool arAvailable;
        public PlayerDetails currentGameState;
        
        public void SetVariableAndSave<T>(ref T variable, T value)
        {
            variable = value;
            //Save(JsonUtility.ToJson(this));
        }
    }
}