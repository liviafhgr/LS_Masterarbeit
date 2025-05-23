using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    //Ich hole alle Buttons und Audio Clips in das Skript rein.
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

//Logik:
//gleiches Prinzip wie bei VideoMessage
//Ich hole das passende Audio Clips in das Skript rein. -> In Unity definiert. Darum auch ItalianAudio und DialectAudio in History.
//Ich hole alle Buttons in das Skript rein.
//Ich hole das passende Button (Sufix/Prefix) in das Skript rein.
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
//Gleichh wie bei VideoMessage aber zwei Audio Clips.
//Ich hole alle Audio Clips in das Skript rein.
//Ich hole alle Audio Clips in das Skript rein (true= auch die inaktiven)
    private Dictionary<string, AudioSource> GetAllAudioClips()
    {
        Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
        foreach (AudioSource audio in GetComponentsInChildren<AudioSource>(true))
        {
            audioSources[audio.gameObject.name] = audio;
        }
        return audioSources;
    }
//Ich hole alle Childobjekte vom Typ Buttons (Play und Pause) in das Skript rein.
//Ich lese pro Button einen Namen des Dictionaryeintrags.
//Sufix:Alle Buttons sind mit Play oder Pause benannt.
//Präfix: Alle Buttons sind mit Italian oder Dialect bennant.
//Prefix + Suffix: Name des Buttons
//AI: Regex.Replace (Claude) entfernt die Endung Play oder Pause (Sufix).
//Das OnClick Event wird mit der Funktion TogglePausePlayButtons verknüpft.
//Das OnClick Event wird mit dem Namen des Buttons verknüpft.
//Hole meinen Bruderbutton ItalianPlay und ItalianPause.
//Setze mich selbst inaktiv (ItlianPlay)
//Setze meinen Bruder aktiv (ItalienPause)
//Spiele oder pausiere den Audio Clip (je nach Event)

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
