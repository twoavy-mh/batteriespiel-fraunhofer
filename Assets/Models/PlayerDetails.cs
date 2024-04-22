using System;
using System.Linq;
using Helpers;
using UnityEditor;
using UnityEngine;

namespace Models
{

    public class PlayerRegistrationRequest
    {
        public string name;
        public string id;
        public Language language;
        public bool finishedIntro;
        public GameState.Models current3dModel;
        
        public PlayerRegistrationRequest(string name, Language language)
        {
            this.name = name;
            id = Guid.NewGuid().ToString();
            this.language = language;
            finishedIntro = false;
            current3dModel = GameState.Models.Cells;
        }
    }

    [Serializable]
    public class PlayerUpdateDetails
    {
        public string id;
        public string name;
        public Language language;
        public bool finishedIntro;
        public GameState.Models current3dModel;
        
        private PlayerUpdateDetails(PlayerDetails player)
        {
            id = player.id;
            name = player.name;
            language = player.language;
            finishedIntro = player.finishedIntro;
            current3dModel = player.current3dModel;
        }
        
        public static explicit operator PlayerUpdateDetails(PlayerDetails v)
        {
            return new PlayerUpdateDetails(v);
        }
    }
    
    [Serializable]
    public class PlayerDetails
    {
        public string id;
        public string name;
        public Language language;
        public int? tradeShowCode;
        public bool finishedIntro;
        public GameState.Models current3dModel;
        public int totalScore;
        public MicrogameState[] results;

        public override string ToString()
        {
            return "PlayerDetails{" +
                   "id='" + id + '\'' +
                   ", name='" + name + '\'' +
                   ", language=" + language +
                   ", tradeShowCode=" + tradeShowCode +
                   ", finishedIntro=" + finishedIntro +
                   ", current3dModel=" + current3dModel +
                   ", totalScore=" + totalScore +
                   ", microgames=" + string.Concat(results.Select(x => x.ToString())) +
                   '}';
        }

        public static explicit operator PlayerDetails(string v)
        {
            PlayerDetails casted = JsonUtility.FromJson<PlayerDetails>(v);
            if (casted.tradeShowCode == 0)
            {
                casted.tradeShowCode = -1;
            }
            return casted;
        }
    }
    
    [Serializable]
    public class MicrogameState
    {
        public GameState.Microgames game;
        public bool unlocked;
        public bool finished;
        public int result;
        public int jumpAndRunResult;

        public override string ToString()
        {
            return "MicrogameState{" +
                   "game=" + game +
                   ", unlocked=" + unlocked +
                   ", finished=" + finished +
                   ", result=" + result +
                   ", jumpNRunResult=" + jumpAndRunResult +
                   "}";
        }
    }

    public class PlayerRegistration : PlayerDetails
    {
        public string token;

        public override string ToString()
        {
            return "PlayerRegistration{" +
                   "id='" + id + '\'' +
                   ", name='" + name + '\'' +
                   ", language=" + language +
                   ", finishedIntro=" + finishedIntro +
                   ", current3dModel=" + current3dModel +
                   ", token='" + token + '\'' +
                   ", totalScore=" + totalScore +
                   '}';
        }

        public static explicit operator PlayerRegistration(string v)
        {
            PlayerRegistration casted = JsonUtility.FromJson<PlayerRegistration>(v);
            return casted;
        }
    }
}