using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fair
{
    public class IdleTimer : MonoBehaviour
    {
        public int idleTime = 180;
        private int _idleTime;
        
        private void Start()
        {
            _idleTime = idleTime;
            InvokeRepeating(nameof(Decrement), 1f, 1f);
        }
        
        private void Update()
        {
            if (Input.anyKey)
            {
                _idleTime = idleTime;
            }
        }

        private void Decrement()
        {
            _idleTime--;
            if (_idleTime == 0)
            {
                if (!SceneManager.GetActiveScene().name.Equals("Onboarding"))
                {
                    PlayerPrefs.DeleteKey("uuid");
                    SceneManager.LoadScene("Onboarding");
                }
                else
                {
                    _idleTime = idleTime;
                }
            }
        }
    }
}