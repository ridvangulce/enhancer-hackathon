using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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