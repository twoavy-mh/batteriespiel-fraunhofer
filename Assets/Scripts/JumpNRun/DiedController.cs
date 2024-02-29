using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiedController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("JumpNRun"));   
        transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));   
    }
}
