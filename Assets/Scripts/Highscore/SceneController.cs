using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers;
using Models;
using UnityEngine;

namespace Highscore
{
    public class SceneController : MonoBehaviour
    {
        // Start is called before the first frame update
        private static SceneController _instance;
        public Leaderboard leaderboard;
        public LeaderboardEntry myScore;
                
        public static SceneController Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.Log("no SceneController yet");
                    return null;
                };
                return _instance;
            }
        }

        // Start is called before the first frame update
        private void Awake()
        {
            StartCoroutine(Api.Instance.GetLeaderboard(PlayerPrefs.GetString("uuid"), array =>
            {
                leaderboard = Leaderboard.ConstructLeaderboard(array);
                Debug.Log(leaderboard.ToString());
                myScore = leaderboard.entries.Single(l => l.isMe);
            }));
            _instance = this;
        }
        
    }   
}
