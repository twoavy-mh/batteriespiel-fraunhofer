using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers;
using Models;
using UnityEngine;

namespace Minigame2
{
    public class SceneController : MonoBehaviour
    {
        // Start is called before the first frame update
        private static SceneController _instance;
        private Dictionary<string, bool> _dropzones = new Dictionary<string, bool>();

        public int fails = 0;
        public GameObject finishedModal;
        
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
            
            _instance = this;
        }

        public async Task DroppedCorrectly(string field)
        {
            _dropzones[field] = true;
            if (_dropzones["kathode"] && _dropzones["anode"] && _dropzones["lithium"] && _dropzones["separator"] && _dropzones["electrode"] && _dropzones["electrolyte"])
            {
                finishedModal.SetActive(true);
                int score = finishedModal.GetComponent<MicrogameTwoFinished>().SetScore(fails, fails + 6);
                MicrogameState s = new MicrogameState();
                s.finished = true;
                s.unlocked = true;
                s.result = score;
                s.jumpAndRunResult = GameState.Instance.currentGameState.results[1].jumpAndRunResult;
                s.game = GameState.Microgames.Microgame2;
                GameState.Instance.currentGameState = await Api.SetGame(s, GameState.Instance.currentGameState.id);
            }
        }
        
    }   
}
