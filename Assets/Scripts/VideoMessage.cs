using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
//Code selber geschrieben
//Wurde automatisch durch add Component new Skript erstellt.
//Test
public class VideoMessage : MonoBehaviour
{
    public Dictionary<string, Button> videoButtons = new Dictionary<string, Button>();
    public VideoPlayer videoMessage = new VideoPlayer();
    private Dictionary<string, Coroutine> videoPlayTracker = new Dictionary<string, Coroutine>();
    // Start is called before the first frame update
    //Startframe: ButtenVideo und Videoclip wird geladen.
    void Start()
    {
        videoButtons = GetAllVideoButtons();
        videoMessage = GetVideoClip();
    }

    // Update is called once per frame
    void Update()
    {
        string[] foobar = new string[2];
    }
    //Funktion für VideoPlayer GameOject in das Skript zu holen. Wird dann als Variabel Typ Videoplayer abgespeichert. Siehe oben.
    private VideoPlayer GetVideoClip()
    {
        return GetComponentInChildren<VideoPlayer>();
    }
    //OnClick Event Buttons
    //Gehe ins Dictonary
    //Hole beide Buttons (Play und Pause) raus.
    // Video selbst wird als VIdeomessage gehplt
    //Logik: Play Button aktiv -> Pause Button wird aktiv und umgekehrt, Video wird automatisch gestartet.
    //Wenn der Play Button aktiv ist, wird die Coroutine gestartet. Diese überprüft den Status des Videos.
    //Wenn das Video nicht mehr läuft, wird der Pause Button deaktiviert und der Play Button aktiviert.
    //Wenn der Pause Button aktiv ist, wird der Play Button aktiviert und das Video pausiert.
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
    //StartCoroutine: Durch AI geschrieben (Claude), läuft parrel zum normalen "AST"
    // Durch AI (Claude) geschrieben.
    //Hier wird das Video gestartet. Das Video wird in der Coroutine gecheckt, ob es läuft oder nicht.
    //Wenn das Video nicht mehr läuft, wird der Pause Button deaktiviert und der Play Button aktiviert.
    //Coroutine (Tracker) wird entfernt.
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
    //Hier werden die Gameobjects Button Start und Button Pause in das Skrip mit dieser Funktion geholt.Alle Buttons (Kinder) werden geholt.
    //Weil es meherere Buttons sind hole ich auch die Namen. Mit diesen Namen füllen wir das Dictonary oben.
    // Wir definiieren das OnClick Event für die beiden Buttons. -> Video Play und Video Pause
    //Das OnClick Event wird mit der Funktion TogglePausePlayVideoButtons verknüpft.
    private Dictionary<string, Button> GetAllVideoButtons()
    {
        Dictionary<string, Button> playButtons = new Dictionary<string, Button>();

        foreach (Button b in GetComponentsInChildren<Button>(true))
        {
            string buttonName = b.gameObject.name;
            b.onClick.AddListener(() => TogglePausePlayVideoButtons());
            playButtons[buttonName] = b;
        }
        return playButtons;
    }
}
