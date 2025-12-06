using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPasswort : MonoBehaviour
{
    [Header("Audio-Clips (in gewünschter Reihenfolge)")]
    [SerializeField] private AudioClip audioA;
    [SerializeField] private AudioClip audioB;

    [Header("Optionen")]
    [SerializeField] private bool playOnStart = true;  // Startet automatisch beim Szenenstart
    [SerializeField] private float startDelay = 0f;    // Verzögerung vor dem ersten Start

    private AudioSource source;
    private bool playingA = true; // Start mit A, danach B, dann wieder A, usw.
    private bool waitingForNext = false;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.loop = false; // Wichtig: nur einmal abspielen, dann wechseln
        source.playOnAwake = false;
    }

    void Start()
    {
        if (playOnStart)
        {
            if (startDelay > 0f)
                Invoke(nameof(PlayNext), startDelay);
            else
                PlayNext();
        }
    }

    void Update()
    {
        // Wenn gerade ein Clip lief und nun fertig ist, den nächsten starten
        if (!waitingForNext && !source.isPlaying && source.clip != null)
        {
            waitingForNext = true;
            // kleinen Frame-Delay, um sauberen Übergang sicherzustellen
            Invoke(nameof(PlayNext), 0.01f);
        }
    }

    public void PlayNext()
    {
        // Wähle den nächsten Clip
        source.clip = playingA ? audioA : audioB;

        // Falls Clip fehlt, nichts tun
        if (source.clip == null)
        {
            Debug.LogWarning("AudioPasswort: Kein AudioClip zugewiesen für " + (playingA ? "audioA" : "audioB"));
            waitingForNext = false;
            return;
        }

        source.Play();
        // Nächstes Mal den anderen Clip wählen
        playingA = !playingA;
        waitingForNext = false;
    }

    // Optional: Manuelles Stoppen
    public void StopPlayback()
    {
        source.Stop();
        waitingForNext = false;
    }

    // Optional: Manuelles Starten/Neustarten der Sequenz mit A
    public void RestartSequence()
    {
        playingA = true; // Beginne wieder mit A
        StopPlayback();
        PlayNext();
    }
}
