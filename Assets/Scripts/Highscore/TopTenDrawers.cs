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
        yield return new WaitUntil(() => SceneController.Instance != null && SceneController.Instance.leaderboard != null);
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(Utility.GetDevice() == Device.Desktop ? 583 : 583 * Settings.RESIZE_FACTOR, SceneController.Instance.leaderboard.entries.Length * (
                (Utility.GetDevice() == Device.Desktop ? 140 : 140 * Settings.RESIZE_FACTOR) + 27));
        foreach (LeaderboardEntry leaderboardEntry in SceneController.Instance.leaderboard.entries)
        {
            GameObject block = Instantiate(blockPrefab, transform);
            block.GetComponent<ScoreboardBlockSetter>().SetTexts(leaderboardEntry.name.Trunc(20),
                leaderboardEntry.jumpAndRunScore.ToString(), leaderboardEntry.microGameScore.ToString(),
                leaderboardEntry.totalScore.ToString().PadLeft(5, '0'), leaderboardEntry.rank.ToString(), leaderboardEntry.isMe);
            LayoutRebuilder.ForceRebuildLayoutImmediate(block.GetComponent<RectTransform>());
        }
    }
}