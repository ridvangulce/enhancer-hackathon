using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource oneShotAudioSource;
    public AudioSource loopAudioSource;

    public AudioClip playerDeathSound, enemyDeathSound, loseSound, winSound, hackSound, rotateSound, jumpSound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        oneShotAudioSource.PlayOneShot(clip);
    }

    public void PlayLoop(AudioClip clip)
    {
        loopAudioSource.clip = clip;
        loopAudioSource.loop = true;
        loopAudioSource.Play();
    }

    public void StopLoop()
    {
        loopAudioSource.Stop();
    }
}
