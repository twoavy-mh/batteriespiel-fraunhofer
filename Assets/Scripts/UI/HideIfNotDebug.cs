using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfNotDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!Debug.isDebugBuild)
        {
            gameObject.SetActive(false);
        }
    }
}
