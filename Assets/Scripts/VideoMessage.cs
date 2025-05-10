using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoMessage : MonoBehaviour
{
    public Dictionary<string, Button> videoButtons = new Dictionary<string, Button>();
    public VideoPlayer videoMessage = new VideoPlayer();
    private Dictionary<string, Coroutine> videoPlayTracker = new Dictionary<string, Coroutine>();
    // Start is called before the first frame update
    void Start()
    {
        videoButtons = GetAllVideoButtons();
        videoMessage = GetVideoClip();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private VideoPlayer GetVideoClip()
    {
        return GetComponentInChildren<VideoPlayer>();
    }

     private void TogglePausePlayVideoButtons()
    {
        var playButton = videoButtons["VideoPlay"];
        var pauseButton = videoButtons["VideoPause"];
        var video = videoMessage;
        if (playButton.IsActive())
        {
            playButton.gameObject.SetActive(!playButton.gameObject.activeSelf);
            pauseButton.gameObject.SetActive(!pauseButton.gameObject.activeSelf);
            videoMessage.Play();
            videoPlayTracker[video.gameObject.name] = StartCoroutine(CheckVideoState(playButton, pauseButton, video));
        }
        else
        {
            pauseButton.gameObject.SetActive(!pauseButton.gameObject.activeSelf);
            playButton.gameObject.SetActive(!playButton.gameObject.activeSelf);
            videoMessage.Pause();
        }
    }

    private IEnumerator CheckVideoState(Button playVideoButton, Button pauseVideoButton, VideoPlayer video)
    {
        yield return new WaitUntil(() => video.isPlaying);

        while (video.isPlaying)
        {
            // audio now started
            yield return null;
        }

        if (!video.isPlaying && pauseVideoButton.IsActive())
        {
            pauseVideoButton.gameObject.SetActive(false);
            playVideoButton.gameObject.SetActive(true);
        }

        videoPlayTracker.Remove(video.gameObject.name);
    }

    private Dictionary<string, Button> GetAllVideoButtons()
    {
        Dictionary<string, Button> playButtons = new Dictionary<string, Button>();

        foreach (Button b in GetComponentsInChildren<Button>(true))
        {
            string buttonName = b.gameObject.name;
            string buttonPrefix = Regex.Replace(buttonName, @"Play$|Pause$", String.Empty);
            b.onClick.AddListener(() => TogglePausePlayVideoButtons());
            playButtons[buttonName] = b;
        }
        return playButtons;
    }
}
