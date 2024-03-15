using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Localization.Settings;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public Button playButton;
    public Button pauseButton;
    public Button replayButton;
    public GameObject skipButton;

    public RectTransform progressBar;
    public TMP_Text videoTime;
    [CanBeNull] public Nullable<float> maxTime = null;


    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayVideo);
        pauseButton.onClick.AddListener(PauseVideo);
        replayButton.onClick.AddListener(ReplayVideo);
#if UNITY_WEBGL
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += VideoPrepared;
#else
        PlayVideo();
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_WEBGL
        if (maxTime != null)
        {
            progressBar.localScale = new Vector3((float)(videoPlayer.time / maxTime), 1, 1);
            videoTime.text =
                $"{((int)videoPlayer.time / 60).ToString().PadLeft(2, '0')}:{((int)videoPlayer.time % 60).ToString().PadLeft(2, '0')} / {((int)videoPlayer.time / 60).ToString().PadLeft(2, '0')}:{((int)maxTime % 60).ToString().PadLeft(2, '0')}";
        }
#else
        progressBar.localScale = new Vector3((float)(videoPlayer.time / videoPlayer.clip.length), 1, 1);
        videoTime.text =
            $"{((int)videoPlayer.time / 60).ToString().PadLeft(2, '0')}:{((int)videoPlayer.time % 60).ToString().PadLeft(2, '0')} / {((int)videoPlayer.clip.length / 60).ToString().PadLeft(2, '0')}:{((int)videoPlayer.clip.length % 60).ToString().PadLeft(2, '0')}";
#endif
    }

    private void PlayVideo()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    public void VideoPrepared(VideoPlayer v)
    {
        maxTime = (v.frameCount / v.frameRate);
        PlayVideo();
    }

    private void PauseVideo()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
        pauseButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        videoPlayer.Pause();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        videoPlayer.loopPointReached -= OnVideoEnd;

        foreach (ImageColorSetter colorSetterInChild in skipButton.GetComponentsInChildren<ImageColorSetter>())
        {
            if (colorSetterInChild.name == "Image")
            {
                colorSetterInChild.UpdateColor(Tailwind.Yellow3);
            }
            else if (colorSetterInChild.name == "MenuIcon")
            {
                colorSetterInChild.UpdateColor(Tailwind.BlueUI);
            }
        }

        skipButton.GetComponentInChildren<FontStyler>().fontColor = Tailwind.BlueUI;

        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(true);

        videoPlayer.time = 0f;
    }

    private void ReplayVideo()
    {
        replayButton.gameObject.SetActive(false);
        PlayVideo();
    }
}