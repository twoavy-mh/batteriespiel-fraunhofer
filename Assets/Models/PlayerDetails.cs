using System.Net.Http;
using Helpers;
using UnityEditor;
using UnityEngine;

namespace Models
{

    public class PlayerRegistrationRequest
    {
        public string name;
        public string id;
        public string language;
        public bool finishedIntro;
        public GameState.Models current3dModel;
        
        public PlayerRegistrationRequest(string name)
        {
            this.name = name;
            id = System.Guid.NewGuid().ToString();
            language = Application.systemLanguage == SystemLanguage.German ? "de" : "en";
            finishedIntro = false;
            current3dModel = GameState.Models.Cells;
        }
    }
    
    public class PlayerDetails
    {
        public string id;
        public string name;
        public Language language;
        public bool finishedIntro;
        public GameState.Models current3dModel;
        public string createdAt;
        public string updatedAt;
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
                   ", createdAt='" + createdAt + '\'' +
                   ", updatedAt='" + updatedAt + '\'' +
                   ", token='" + token + '\'' +
                   '}';
        }

        public static explicit operator PlayerRegistration(string v)
        {
            PlayerRegistration casted = JsonUtility.FromJson<PlayerRegistration>(v);
            Debug.Log(casted.ToString());
            return casted;
        }
    }
}