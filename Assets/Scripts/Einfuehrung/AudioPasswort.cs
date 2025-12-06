using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPasswort : MonoBehaviour
{
    [Header("Audio-Clips (in gewünschter Reihenfolge)")]
    [SerializeField] private AudioClip audioA;
    [SerializeField] private AudioClip audioB;

    [Header("Optionen")]
    [SerializeField] private bool playOnStart = true;   // Startet automatisch beim Szenenstart
    [SerializeField] private float startDelay = 0f;     // Verzögerung vor dem ersten Start
    [SerializeField] private int repeatCycles = 3;      // Anzahl der Wechsel-Zyklen (A->B zählt als 1 Zyklus)

    private AudioSource source;
    private bool playingA = true;       // Start mit A, danach B, dann wieder A, usw.
    private bool waitingForNext = false;
    private int completedCycles = 0;    // Anzahl abgeschlossener A->B Zyklen
    private bool finished = false;      // Sequenz beendet

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
        if (finished) return;

        // Wenn gerade ein Clip lief und nun fertig ist, den nächsten starten
        if (!waitingForNext && !source.isPlaying && source.clip != null)
        {
            waitingForNext = true;

            // Prüfe, ob ein Zyklus abgeschlossen wurde (nachdem B abgespielt wurde)
            // Zyklus-Definition: A gefolgt von B -> 1 Zyklus
            if (!playingA) // Wir haben gerade B gespielt (da playingA vorher auf false gesetzt wurde)
            {
                completedCycles++;
                if (completedCycles >= repeatCycles)
                {
                    finished = true;
                    return; // nicht mehr weiter abspielen
                }
            }

            // kleinen Frame-Delay, um sauberen Übergang sicherzustellen
            Invoke(nameof(PlayNext), 0.01f);
        }
    }

    public void PlayNext()
    {
        if (finished) return;

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
        finished = true;
    }

    // Optional: Manuelles Starten/Neustarten der Sequenz mit A
    public void RestartSequence()
    {
        playingA = true; // Beginne wieder mit A
        completedCycles = 0;
        finished = false;
        StopPlayback();
        PlayNext();
    }
}
