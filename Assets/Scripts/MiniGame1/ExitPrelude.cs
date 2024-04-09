using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPrelude : MonoBehaviour
{
    
    public GameObject prelude;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(C);
    }

    private void C()
    {
        GameObject.Find("Video Player").GetComponent<Timeslot>().startGame = true;
        prelude.SetActive(false);
    }
    
}
