using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class RenderUiBasedOnDevice : MonoBehaviour
{
    public GameObject[] mobileObjects;
    public GameObject[] desktopObjects;

    public bool doInstant = true;

    void Start()
    {
        if (doInstant)
        {
            DoIt();
        }
    }

    public GameObject[] DoIt()
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
        
        return isMobile ? mobileObjects : desktopObjects;
    }
}