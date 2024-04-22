using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DestroyOnClick : MonoBehaviour
    {
        public GameObject toDestroy;
        
        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(DestroyFunc);
        }
        
        private void DestroyFunc()
        {
            Destroy(toDestroy);
        }
    }
}