using System.Collections;
using System.Collections.Generic;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardBlockSetter : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text scoreTextJnR;
    public TMP_Text scoreTextGames;
    public TMP_Text scoreTextTotal;
    public TMP_Text rankText;
    public Image background;
    
    public void SetTexts(string nameT, string scoreJnR, string scoreGames, string scoreTotal, string rank, bool isOwn)
    {
        nameText.text = nameT;
        scoreTextJnR.text = scoreJnR;
        scoreTextGames.text = scoreGames;
        scoreTextTotal.text = scoreTotal;
        rankText.text = rank;
        background.color = Settings.ColorMap[isOwn ? Tailwind.Blue5 : Tailwind.Blue4];
    }
}
