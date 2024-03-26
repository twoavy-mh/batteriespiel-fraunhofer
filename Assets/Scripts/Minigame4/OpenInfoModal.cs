using System.Collections;
using System.Collections.Generic;
using Minigame4;
using UnityEngine;
using UnityEngine.UI;

public class OpenInfoModal : MonoBehaviour
{
    public GameObject modal;
    public string header;
    public string text;
    public Sprite sprite;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenModal);
    }
    
    void OpenModal()
    {
        modal.SetActive(true);
        modal.GetComponent<ModalManager>().SetContent(header, text, sprite);
    }
}
