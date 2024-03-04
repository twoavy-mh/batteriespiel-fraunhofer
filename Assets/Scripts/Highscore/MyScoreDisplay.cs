using System.Collections;
using System.Collections.Generic;
using Highscore;
using UnityEngine;

public class MyScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => SceneController.Instance != null);
        GetComponent<ScoreboardBlockSetter>().SetTexts(SceneController.Instance.myScore.name,
            SceneController.Instance.myScore.totalScore.ToString(),
            SceneController.Instance.myScore.totalScore.ToString(),
            SceneController.Instance.myScore.totalScore.ToString().PadLeft(5, '0'),
            SceneController.Instance.myScore.rank.ToString(), true);
    }
}