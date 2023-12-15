using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public Button m_MainMenuButton;
    public RectTransform m_MainMenuTransform;
    
    private Boolean menuOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_MainMenuButton.onClick.AddListener(toggleMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void toggleMenu()
    {
        menuOpen = !menuOpen;
        if (menuOpen)
        {
            m_MainMenuTransform.position = new Vector2(16f, m_MainMenuTransform.position.y);
        }
        else
        {
            m_MainMenuTransform.position = new Vector2(Screen.width + 16f, m_MainMenuTransform.position.y);

        }
    }
}
