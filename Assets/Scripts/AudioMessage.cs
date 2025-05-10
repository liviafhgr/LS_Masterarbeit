using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioMessage : MonoBehaviour
{
    public Dictionary<string, Button> buttonDictionary = new Dictionary<string, Button>();
    public Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, Coroutine> audioCoroutineTracker = new Dictionary<string, Coroutine>();
    // Start is called before the first frame update
    void Start()
    {
        buttonDictionary = GetAllPlayButtons();
        audioSources = GetAllAudioClips();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator CheckAudioState(Button playButton, Button pauseButton, AudioSource audio)
    {
        // Wait for audio stream to start play
        yield return new WaitUntil(() => audio.isPlaying);

        while (audio.isPlaying)
        {
            // audio now started
            yield return null;
        }

        if (!audio.isPlaying && pauseButton.IsActive())
        {
            pauseButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }

        audioCoroutineTracker.Remove(audio.gameObject.name);
    }

    private void TogglePausePlayButtons(string buttonPrefix)
    {
        var playButton = buttonDictionary[buttonPrefix + "Play"];
        var pauseButton = buttonDictionary[buttonPrefix + "Pause"];
        var audio = audioSources[buttonPrefix + "Audio"];
        if (playButton.IsActive())
        {
            playButton.gameObject.SetActive(!playButton.gameObject.activeSelf);
            pauseButton.gameObject.SetActive(!pauseButton.gameObject.activeSelf);
            audio.Play();
            audioCoroutineTracker[audio.gameObject.name] = StartCoroutine(CheckAudioState(playButton, pauseButton, audio));
        }
        else
        {
            pauseButton.gameObject.SetActive(!pauseButton.gameObject.activeSelf);
            playButton.gameObject.SetActive(!playButton.gameObject.activeSelf);
            audio.Pause();
        }
    }

    private Dictionary<string, AudioSource> GetAllAudioClips()
    {
        Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
        foreach (AudioSource audio in GetComponentsInChildren<AudioSource>(true))
        {
            audioSources[audio.gameObject.name] = audio;
        }
        return audioSources;
    }

    private Dictionary<string, Button> GetAllPlayButtons()
    {
        Dictionary<string, Button> playButtons = new Dictionary<string, Button>();

        foreach (Button b in GetComponentsInChildren<Button>(true))
        {
            string buttonName = b.gameObject.name;
            string buttonPrefix = Regex.Replace(buttonName, @"Play$|Pause$", String.Empty);
            b.onClick.AddListener(() => TogglePausePlayButtons(buttonPrefix));
            playButtons[buttonName] = b;
        }
        return playButtons;
    }

}
