using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class InitMainMenu : MonoBehaviour
{
    Camera m_MainCamera;

    void Start()
    {
        m_MainCamera = Camera.main;
        
        m_MainCamera.clearFlags = CameraClearFlags.SolidColor;
        m_MainCamera.backgroundColor = Settings.ColorMap[Tailwind.Blue5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
