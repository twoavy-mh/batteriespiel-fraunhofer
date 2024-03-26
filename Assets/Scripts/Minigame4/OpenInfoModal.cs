using System.Collections;
using System.Collections.Generic;
using Minigame4;
using UnityEngine;
using UnityEngine.UI;

public class OpenInfoModal : MonoBehaviour
{
    public string header;
    public string text;
    public Sprite sprite;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenModal);
    }
    
    void OpenModal()
    {
        GameObject[] modal = GetComponent<RenderUiBasedOnDevice>().DoIt();
        modal[0].SetActive(true);
        modal[0].GetComponent<ModalManager>().SetContent(header, text, sprite);
    }
}
