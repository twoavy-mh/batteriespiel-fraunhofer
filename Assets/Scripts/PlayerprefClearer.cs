using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerprefClearer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ClearPlayerPrefs);
    }

    void ClearPlayerPrefs()
    {
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
        {
            PlayerPrefs.DeleteKey("uuid");
            return;
        }
        PlayerPrefs.DeleteAll();
    }
}
