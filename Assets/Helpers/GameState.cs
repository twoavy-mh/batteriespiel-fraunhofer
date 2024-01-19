using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    
    [Serializable]
    public class GameState : Persistable
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
        
        public string uuid;
        public string name;
        public int totalScore;
        public Language language;
        public bool finishedIntro;
        public string current3dModel;
        
        public void SetVariableAndSave<T>(ref T variable, T value)
        {
            variable = value;
            Save(JsonUtility.ToJson(this));
        }
        
        public MicrogameState[] microgames = new MicrogameState[5];

        public void Init()
        {
            
            SetVariableAndSave<string>(ref uuid, "lololololol");
            Debug.Log(uuid);
            Debug.Log(CheckUuid());
            if (GameplayExists())
            {
                MapData(Load());
            }
        }

        private bool GameplayExists()
        {
            return true;
        }

        private void MapData(GameState exisitingState)
        {
            uuid = exisitingState.uuid;
            name = exisitingState.name;
            totalScore = exisitingState.totalScore;
            language = exisitingState.language;
            finishedIntro = exisitingState.finishedIntro;
            current3dModel = exisitingState.current3dModel;
            microgames = exisitingState.microgames;
        }
    }
    
    [Serializable]
    public class MicrogameState
    {
        public GameState.Microgames game;
        public bool unlocked;
        public bool finished;
        public int score;
    }
    
    public abstract class Persistable
    {
        protected string CheckUuid()
        {
            string s = PlayerPrefs.GetString("uuid");
            if (s.Empty())
            {
                s = Register();    
            }

            return s;
        }
        protected string Register()
        {
            string s = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("uuid", s);
            return s;
        }
        
        protected void Save(string json)
        {
            Debug.Log("Fake api call: " + json);
        }
        
        protected GameState Load()
        {
            string fakeString = "{}";
            return JsonUtility.FromJson<GameState>(fakeString);
        }

        protected bool Verify()
        {
            Debug.Log("Fake api call");
            return true;
        }
    }
}