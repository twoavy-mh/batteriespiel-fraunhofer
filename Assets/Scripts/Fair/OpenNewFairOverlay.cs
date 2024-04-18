using UnityEngine;
using UnityEngine.UI;

namespace Fair
{
    
    [RequireComponent(typeof(Button))]
    public class OpenNewFairOverlay : MonoBehaviour
    {
        public GameObject newFairOverlay;
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(BtnOpenNewFairOverlay);
        }
        
        private void BtnOpenNewFairOverlay()
        {
            Instantiate(newFairOverlay);
        }
        
    }
}