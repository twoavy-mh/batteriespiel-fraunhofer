using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSettingsBasedOnPlatform : MonoBehaviour
{
    
    public VideoClip videoClip;
    public string videoUrl;
    
    void Awake()
    {
        //&& !UNITY_EDITOR
        #if UNITY_WEBGL
        GetComponent<VideoPlayer>().source = VideoSource.Url;
        GetComponent<VideoPlayer>().url = videoUrl;
        #else
        GetComponent<VideoPlayer>().source = VideoSource.VideoClip;
        GetComponent<VideoPlayer>().clip = videoClip;
        #endif
    }
}
