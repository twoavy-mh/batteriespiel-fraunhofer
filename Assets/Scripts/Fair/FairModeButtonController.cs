using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fair
{
    public class FairModeButtonController : MonoBehaviour
    {
        public GameObject fairModeButton;
        public GameObject newFairButton;
        public GameObject newFairOverlay;

        private bool _couldCreateNewFairCode = false;

        // Start is called before the first frame update
        void Start()
        {
#if UNITY_STANDALONE_WIN
            fairModeButton.SetActive(false);
            newFairButton.SetActive(false);
            _couldCreateNewFairCode = true;
#elif UNITY_EDITOR
            fairModeButton.SetActive(false);
            newFairButton.SetActive(false);
            _couldCreateNewFairCode = true;
#else
            newFairButton.SetActive(false);
#endif
        }

        private void BtnOpenNewFairOverlay()
        {
            if (GameObject.Find("CreateNewFairModal") == null)
            {
                Instantiate(newFairOverlay).name = "CreateNewFairModal";
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_couldCreateNewFairCode && Input.GetKeyDown("m"))
            {
                BtnOpenNewFairOverlay();
            }
        }
    }
}
