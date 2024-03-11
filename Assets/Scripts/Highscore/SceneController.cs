using System.Collections;
using System.Collections.Generic;
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
            leaderboard = Leaderboard.ConstructLeaderboard();
            myScore = leaderboard.entries[0];
            _instance = this;
        }
        
    }   
}
