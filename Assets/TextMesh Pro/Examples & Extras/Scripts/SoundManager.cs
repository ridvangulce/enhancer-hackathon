using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource oneShotAudioSource;
    public AudioSource loopAudioSource;

    public AudioClip introLoopSound,
        classicLoopSound,
        playerDeathSound,
        enemyDeathSound,
        loseSound,
        winSound,
        hackSound,
        rotateSound,
        jumpSound,
        computingSound;

    private void Awake()
    {
        loopAudioSource.mute = false;
        loopAudioSource.Play();
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

    public void StopOneShot()
    {
        oneShotAudioSource.Stop();
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