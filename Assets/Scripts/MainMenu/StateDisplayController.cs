using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateDisplayController : MonoBehaviour
{
    
    public Sprite[] sprites;
    public Button playJumpAndRun;
    public Image imageState;
    
    void Start()
    {
        switch (GameState.Instance.currentGameState.results.Length)
        {
            case 1:
            case 2:
                imageState.sprite = sprites[0];
                break;
            case 3:
            case 4:
                imageState.sprite = sprites[1];
                break;
            case 5:
                imageState.sprite = sprites[2];
                break;
        }
        imageState.SetNativeSize();
        playJumpAndRun.onClick.AddListener(PlayJumpAndRun);
    }

    private void PlayJumpAndRun()
    {
        SceneManager.LoadScene("JumpNRun");
    }
}