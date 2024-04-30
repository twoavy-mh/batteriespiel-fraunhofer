using System.Collections;
using System.Collections.Generic;
using Helpers;
using UI;
using UnityEngine;

public class ShowInitialHelp : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        int i = (int)GameState.Instance.GetCurrentMicrogame() + 1;
        if (i == 6)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject.Find("Canvas").GetComponentInChildren<LifeBar>().StartGame();
            GameObject.Find("StartScreen").SetActive(false);
        } else
        {
            GetComponent<SelfTranslatingText>().translationKey = $"jnr_lvl_{i}_help";
        }
    }
}
