using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickToChangeSzene : MonoBehaviour
{
    [SerializeField]
    public Button yourButton;
    
    [SerializeField]
    public Object jumpScene;

    void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(LoadSzene);
    }

    void LoadSzene()
    {
        SceneManager.LoadScene(jumpScene.name);
    }
}
