using System;
using System.Collections;
using System.Collections.Generic;
using Minigame2;
using UnityEngine;
using UnityEngine.UI;

public class EndGame2Prematurely : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Btn);    
    }

    void Btn()
    {
        SceneController.Instance.End();
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveListener(Btn);
    }
}
