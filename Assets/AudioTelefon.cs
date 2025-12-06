using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioTelefon : StateMachineBehaviour
{
    [SerializeField] private string sceneToLoad = "abschlussanimation"; // Zielszene
    [SerializeField] private float delaySeconds = 5f;                   // Wartezeit vor Audio-Start

    private AudioSource phoneAudio;
    private bool playbackStarted;   // Audio hat begonnen
    private bool loadTriggered;     // Szenenwechsel bereits ausgelöst
    private float enterTime;        // Zeitpunkt des State-Eintritts

    // Wird aufgerufen, wenn der State betreten wird
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // AudioSource am Animator-Objekt oder seinen Kindern suchen
        phoneAudio = animator.GetComponent<AudioSource>();
        if (phoneAudio == null)
            phoneAudio = animator.GetComponentInChildren<AudioSource>();

        playbackStarted = false;
        loadTriggered = false;
        enterTime = Time.time;

        if (phoneAudio != null)
        {
            phoneAudio.loop = false;                // Clip nur einmal abspielen
            phoneAudio.PlayDelayed(delaySeconds);   // Verzögerter Start
            Debug.Log($"AudioTelefon: Audio startet in {delaySeconds}s: {phoneAudio.gameObject.name}");
        }
        else
        {
            Debug.LogWarning("AudioTelefon: Keine AudioSource am Animator-Objekt oder seinen Kindern gefunden.");
        }
    }

    // Wird in jedem Frame aufgerufen, solange der State aktiv ist
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (loadTriggered || phoneAudio == null) return;

        // Wurde das Audio nach der Verzögerung tatsächlich gestartet?
        if (!playbackStarted)
        {
            // Warte bis die Verzögerung vorbei ist und isPlaying true wird
            if (Time.time >= enterTime + delaySeconds && phoneAudio.isPlaying)
            {
                playbackStarted = true;
                // Debug.Log("AudioTelefon: Audio hat begonnen zu spielen.");
            }
        }
        else
        {
            // Audio war gestartet und ist nun fertig -> Szene laden
            if (!phoneAudio.isPlaying)
            {
                loadTriggered = true;
                Debug.Log($"AudioTelefon: Audio beendet. Lade Szene: {sceneToLoad}");
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    // Aufräumen beim Verlassen des States (optional)
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        phoneAudio = null;
        playbackStarted = false;
        loadTriggered = false;
    }
}
