using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class RenderUiBasedOnDevice : MonoBehaviour
{
    
    public GameObject[] mobileObjects;
    public GameObject[] desktopObjects;
    
    void Start()
    {
        bool isMobile = Utility.GetDevice() == Device.Mobile;
        foreach (GameObject mobileObject in mobileObjects)
        {
            mobileObject.SetActive(isMobile);
        }

        foreach (GameObject desktopObject in desktopObjects)
        {
            desktopObject.SetActive(!isMobile);
        }
    }
}
