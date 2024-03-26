using System.Collections;
using System.Collections.Generic;
using Minigame4;
using UnityEngine;
using UnityEngine.UI;

public class OpenInfoModal : MonoBehaviour
{
    private GameObject _modal;
    public string header;
    public string text;
    public Sprite sprite;
    
    void Start()
    {
        //_modal = GameObject.Find("Modal");
        GetComponent<Button>().onClick.AddListener(OpenModal);
        Debug.Log("DID THIS MOUNT?");
    }
    
    void OpenModal()
    {
        Debug.Log("HELLOOO????");
        //_modal.SetActive(true);
        //_modal.GetComponent<ModalManager>().SetContent(header, text, sprite);
    }
}
