using System.Collections.Generic;
using Helpers;
using Models;
using TMPro;
using UnityEngine;

namespace Minigame2
{
    public class SceneController : MonoBehaviour
    {
        // Start is called before the first frame update
        private static SceneController _instance;
        private Dictionary<string, bool> _dropzones = new Dictionary<string, bool>();
        public GameObject modalGo;

        public int fails = 0;

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
            _dropzones.Add("lith_discharge", false);
            _dropzones.Add("separator", false);
            _dropzones.Add("lith_charge", false);
            _dropzones.Add("electrolyte", false);

            _instance = this;
        }

        public void DroppedCorrectly(string field)
        {
            _dropzones[field] = true;
            if (_dropzones["kathode"] && _dropzones["anode"] && _dropzones["lith_discharge"] &&
                _dropzones["separator"] && _dropzones["lith_charge"] && _dropzones["electrolyte"])
            {
                GameObject[] all = GetComponent<RenderUiBasedOnDevice>().DoIt();
                //TODO: figure out why this is undefined below
                int score = all[0].GetComponent<MicrogameTwoFinished>().SetScore(fails, fails + 6);
                Debug.Log(all[0].transform.Find("Body"));
                Utility.GetTranslatedText(score > 60 ? "microgame_2_did_good" : "microgame_2_did_bad",
                    (s) => all[0].transform.Find("Body").GetComponent<TMP_Text>().text = s);
                MicrogameState s = new MicrogameState();
                s.finished = true;
                s.unlocked = true;
                s.result = score;
                s.jumpAndRunResult = GameState.Instance.currentGameState.results[1].jumpAndRunResult;
                s.game = GameState.Microgames.Microgame2;
                StartCoroutine(Api.Instance.SetGame(s, GameState.Instance.currentGameState.id, details =>
                {
                    GameState.Instance.currentGameState = details;
                }));
            }
        }
    }
}