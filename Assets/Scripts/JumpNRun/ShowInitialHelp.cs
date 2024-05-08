using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UI;
using UnityEngine;

public class ShowInitialHelp : MonoBehaviour
{
    public int currentMicrogame = 0;

    public SelfTranslatingText helpText;
    
    public GameObject firstLevelHelp;
    
    [SerializeField]
    public List<GameObject> energyIcons;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        currentMicrogame = Math.Min((int)GameState.Instance.GetCurrentMicrogame() + 1, 6);
        if (currentMicrogame == 6)
        {
            yield return new WaitForSeconds(0.0f);
            GameObject.Find("Canvas").GetComponentInChildren<LifeBar>().StartGame();
            GameObject.Find("StartScreen").SetActive(false);
        } else
        {
            helpText.translationKey = $"jnr_lvl_{currentMicrogame}_help";
        }
            
        SwitchCurrentEnergy();
    }

    private void SwitchCurrentEnergy()
    {
        switch (currentMicrogame)
        {
            case 1:
                firstLevelHelp.SetActive(true);
                firstLevelHelp.GetComponent<SelfTranslatingText>().translationKey = Utility.GetDevice() == Device.Mobile
                    ? "jnr_lvl_1_help_smartphone"
                    : "jnr_lvl_1_help_desktop";
                energyIcons[0].SetActive(true);
                break;
            case 2:
                energyIcons[1].SetActive(true);
                break;
            case 3:
                energyIcons[2].SetActive(true);
                break;
            case 4:
                energyIcons[3].SetActive(true);
                break;
            case 5:
                energyIcons[4].SetActive(true);
                break;
            default:
                energyIcons[0].SetActive(true);
                energyIcons[1].SetActive(true);
                energyIcons[2].SetActive(true);
                energyIcons[3].SetActive(true);
                energyIcons[4].SetActive(true);
                break;
        }
    }
}
