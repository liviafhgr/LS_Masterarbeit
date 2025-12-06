using UnityEngine;

public class AudioTelefon : StateMachineBehaviour
{
    // Wird aufgerufen, wenn der State betreten wird
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 1) AudioSource direkt am Animator-Objekt suchen
        var phoneAudio = animator.GetComponent<AudioSource>();
        // 2) Falls nicht vorhanden: in den Kindern suchen
        if (phoneAudio == null)
            phoneAudio = animator.GetComponentInChildren<AudioSource>();

        if (phoneAudio != null)
        {
            phoneAudio.loop = false;   // Clip nur einmal abspielen
            // 5 Sekunden warten, dann abspielen
            phoneAudio.PlayDelayed(5f);
            Debug.Log("AudioTelefon: AudioSource gefunden, Abspielung in 5s: " + phoneAudio.gameObject.name);
        }
        else
        {
            Debug.LogWarning("AudioTelefon: Keine AudioSource am Animator-Objekt oder seinen Kindern gefunden.");
        }
    }
}
