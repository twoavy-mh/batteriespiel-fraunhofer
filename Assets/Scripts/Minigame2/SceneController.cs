using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Minigame2
{
    public class SceneController : MonoBehaviour
    {
        // Start is called before the first frame update
        private static SceneController _instance;
        private Dictionary<string, bool> _dropzones = new Dictionary<string, bool>();
        public MultiLang UiTexts = new MultiLang();
        
        public static SceneController Instance
        {
            get
            {
                if (_instance == null) Debug.Log("no SceneController yet");
                return _instance;
            }
        }

        // Start is called before the first frame update
        private void Awake()
        {
            _dropzones.Add("kathode", false);
            _dropzones.Add("anode", false);
            _dropzones.Add("lithium", false);
            _dropzones.Add("separator", false);
            _dropzones.Add("electrode", false);
            _dropzones.Add("electrolyte", false);

            UiTexts.AddMultiple(new[] { "kathode", "anode", "lithium", "separator", "elektrode", "electrolyte" }, new[]
            {
                new Dictionary<Language, string>()
                {
                    { Language.De, "Kathode" },
                    { Language.En, "Cathode" }
                },
                new Dictionary<Language, string>(){
                    {Language.De, "Anode"},
                    {Language.En, "Anode"}
                },
                new Dictionary<Language, string>(){
                    {Language.De, "Lithium"},
                    {Language.En, "Lithium"}
                },
                new Dictionary<Language, string>(){
                    {Language.De, "Separator"},
                    {Language.En, "Separator"}
                },
                new Dictionary<Language, string>(){
                    {Language.De, "Elektrode"},
                    {Language.En, "Electrode"}
                },
                new Dictionary<Language, string>(){
                    {Language.De, "Elektrolyte"},
                    {Language.En, "Electrolytes"}
                }
            });
            
            _instance = this;
        }

        public void DroppedCorrectly(string field)
        {
            _dropzones[field] = true;
        }
        
    }   
}
