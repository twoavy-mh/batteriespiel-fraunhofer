using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowConfirmDelete : MonoBehaviour
{

    public GameObject canvas;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenModal);
    }

    private void OpenModal()
    {
        Instantiate(canvas);
    }
}
