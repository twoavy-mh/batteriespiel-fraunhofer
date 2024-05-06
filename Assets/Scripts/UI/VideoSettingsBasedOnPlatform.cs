using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Video;

public class VideoSettingsBasedOnPlatform : MonoBehaviour
{
    public VideoClip videoClip;
    public VideoClip videoClipEn;
    public string videoUrl;
    public string videoUrlEn;
    public bool isMultiLang;

    void Awake()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        //&& !UNITY_EDITOR
#if UNITY_WEBGL
        videoPlayer.source = VideoSource.Url;
        if (!isMultiLang)
        {
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoUrl);
        }
        else
        {
            if ((GameState.Instance?.currentGameState?.language | Language.De) == Language.En)
            {
                videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoUrlEn);
            }
            else
            {
                videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoUrl);
            }
        }
#else
        videoPlayer.source = VideoSource.VideoClip;
        if (!isMultiLang)
        {
            videoPlayer.clip = videoClip;
        } else {
            if ((GameState.Instance?.currentGameState?.language | Language.De) == Language.En)
            {
                videoPlayer.clip = videoClipEn;
            }
            else
            {
                videoPlayer.clip = videoClip;
            }
}
#endif
    }
}