using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers;
using UnityEngine;

namespace Models
{
    public class Leaderboard
    {
        public LeaderboardArray entries;
        public bool isMeInTop10;
        
        private Leaderboard(LeaderboardArray entries)
        {
            this.entries = entries;
            this.isMeInTop10 = this.entries.sorted.Any((entry) => entry.isMe);
        }
        
        public static async Task<Leaderboard> ConstructLeaderboard()
        {
            LeaderboardArray l = await Api.GetLeaderboard(PlayerPrefs.GetString("uuid"));
            Leaderboard leaderboard = new Leaderboard(l);
            return leaderboard;
        }

        public override string ToString()
        {
            return "Leaderboard{" +
                   "entries=" + string.Concat(entries.sorted.Select(x => x.ToString())) +
                   ", isMeInTop10=" + isMeInTop10 +
                   '}';
        }
    }
    
    [Serializable]
    public class LeaderboardArray
    {
        public LeaderboardEntry[] sorted;
        
        public static explicit operator LeaderboardArray(string v)
        {
            LeaderboardArray casted = JsonUtility.FromJson<LeaderboardArray>("{\"sorted\":" + v + "}");
            return casted;
        }
    }

    [Serializable]
    public class LeaderboardEntry
    {
        public string id;
        public string name;
        public int totalScore;
        public int rank;
        public bool isMe;
        
        public override string ToString()
        {
            return "LeaderboardEntry{" +
                   "id='" + id + '\'' +
                   ", name='" + name + '\'' +
                   ", totalScore=" + totalScore +
                   ", rank=" + rank +
                   ", isMe=" + isMe +
                   '}';
        }
    }
}