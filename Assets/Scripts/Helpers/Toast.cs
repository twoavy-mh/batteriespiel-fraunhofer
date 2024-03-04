using UnityEngine;

namespace Helpers
{
    public class Toast
    {

        private string _msg;
        
        public Toast(string msg)
        {
            _msg = msg;
        }
        
        public void Show()
        {
#if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, "blob", 0);
                    toastObject.Call("show");
                }));
            }
#endif
        }
    }
}