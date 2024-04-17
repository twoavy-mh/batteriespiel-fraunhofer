using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Helpers;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameButtonController : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    
    [SerializeField] public int index;
    
    // Start is called before the first frame update
    void Start()
    {
        /*foreach (MicrogameState microgameState in GameState.Instance.currentGameState.results)
        {
            Debug.Log(microgameState);
        }*/
        try
        {
            bool exists = GameState.Instance.currentGameState.results.ElementAtOrDefault(index - 1) != null;
            if (!exists)
            {
                SetStatus(false);
                return;
            }
            SetStatus(GameState.Instance.currentGameState.results[index - 1].unlocked);
        } catch (IndexOutOfRangeException)
        {
            Debug.Log("index out of bounds");
            SetStatus(false);
        } catch (NullReferenceException)
        {
            Debug.Log("null reference");
            SetStatus(false);
        }
    }
    
    public void SetStatus(bool a_Status)
    {
        Image buttonImage = GameObject.Find("ButtonImage" + index).GetComponent<Image>();;
        TMP_Text text = GameObject.Find("ButtonLabel" + index).GetComponent<TMP_Text>();
        
        if (a_Status)
        {
            text.color = Settings.ColorMap[Tailwind.Yellow3];
            buttonImage.sprite = activeSprite;
        }
        else
        {
            if (!Application.isEditor)
            {
                GetComponent<Button>().interactable = false;
            }
            text.color = Settings.ColorMap[Tailwind.Blue4];
            buttonImage.sprite = inactiveSprite;
        }
    }
    
}
