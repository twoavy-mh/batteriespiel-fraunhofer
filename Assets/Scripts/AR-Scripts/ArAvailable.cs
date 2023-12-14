using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArAvailable : MonoBehaviour
{
    public GameObject btnAr;
    public GameObject btn3DViewer;
    
    public void OnEnable()
    {
        StartCoroutine(ARSession.CheckAvailability());
        StartCoroutine(AllowARScene());
    }
 
    IEnumerator AllowARScene()
    {
        while (true)
        {
            while (ARSession.state == ARSessionState.CheckingAvailability ||
                   ARSession.state == ARSessionState.None)
            {
                Debug.Log("Waiting...");
                yield return null;
            }
            if (ARSession.state == ARSessionState.Unsupported)
            {
                btn3DViewer.SetActive(true);
                Debug.Log("AR unsupported");
                yield break;
            }
            if (ARSession.state > ARSessionState.CheckingAvailability)
            {
                btnAr.SetActive(true);
                Debug.Log("AR supported");
                yield break;
            }
        }      
    }
}
