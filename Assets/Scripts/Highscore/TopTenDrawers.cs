using System.Collections;
using System.Collections.Generic;
using Helpers;
using Highscore;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class TopTenDrawers : MonoBehaviour
{
    public GameObject blockPrefab;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => SceneController.Instance != null);
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(583, SceneController.Instance.leaderboard.entries.Length * (140 + 27));
        foreach (LeaderboardEntry leaderboardEntry in SceneController.Instance.leaderboard.entries)
        {
            GameObject block = Instantiate(blockPrefab, transform);
            block.GetComponent<ScoreboardBlockSetter>().SetTexts(leaderboardEntry.name.Trunc(20),
                leaderboardEntry.totalScore.ToString(), leaderboardEntry.totalScore.ToString(),
                leaderboardEntry.totalScore.ToString().PadLeft(5, '0'), leaderboardEntry.rank.ToString(), leaderboardEntry.isMe);
            LayoutRebuilder.ForceRebuildLayoutImmediate(block.GetComponent<RectTransform>());
        }
    }
}