using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFadeInLoop : MonoBehaviour
{
    public float fadeInDuration = 6f;
    public float maxVolume = 1f;

    private AudioSource audioSource;
    private float timer = 0f;
    private bool fadingIn = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        if (fadingIn)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, maxVolume, timer / fadeInDuration);

            if (timer >= fadeInDuration)
            {
                audioSource.volume = maxVolume;
                fadingIn = false;
            }
        }
    }
}
