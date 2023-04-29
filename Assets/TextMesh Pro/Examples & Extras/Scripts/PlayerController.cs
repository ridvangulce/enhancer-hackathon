using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Player movement speed
    [SerializeField] private float jumpForce = 8f; // Jump force
    [SerializeField] private Transform groundCheck; // Transform object to check if the player is grounded
    [SerializeField] private LayerMask groundLayer; // Layer mask for the ground objects
    [SerializeField] private ParticleSystem _particleSystem; // Layer mask for the ground objects
    [SerializeField] private GameObject _particleSystemObject; // Layer mask for the ground objects
    [SerializeField] private GameObject _finishCanvas;
    [SerializeField] private GameObject _gameOverCanvas;
    float gainX, gainY, gainZ, gammaX, gammaY, gammaZ, liftX, liftY, liftZ;
    public Rigidbody2D rb;
    Animator animator;
    public Transform videoSquareTexture;
    public GameObject enemyExplosion, playerExplosion;
    private int _jumpCounter;
    [HideInInspector] public bool isOver;
    public GameObject timeline;
    public bool canMove;

    void Awake()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.loopAudioSource.Play();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timeline.SetActive(false);
        canMove = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isRunning", true);
            Vector3 xScale = new Vector3(-3, transform.localScale.y, transform.localScale.z);
            transform.localScale = xScale;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", true);
            Vector3 xScale = new Vector3(3, transform.localScale.y, transform.localScale.z);
            transform.localScale = xScale;
        }
        else
        {
            animator.SetBool("isRunning", false);
            CancelInvoke("SpawnParticleEffect");
        }


        // Move player horizontally
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        // Jump when the player presses the spacebar and is grounded
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCounter <= 1)
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.jumpSound);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _jumpCounter++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LimitLine"))
        {
            isOver = true;
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.playerDeathSound);
            StartCoroutine(MuteLoopSound());

            Debug.Log("is Over");
        }

        if (collision.gameObject.CompareTag("destroyText"))
        {
            isOver = true;
            StartCoroutine(MuteLoopSound());
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.playerDeathSound);
            playerExplosion.SetActive(true);
            playerExplosion.transform.position = transform.position;
            _finishCanvas.SetActive(false);
            _gameOverCanvas.SetActive(true);
            CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            capsuleCollider2D.enabled = false;
        }

        if (collision.gameObject.name == "turretRange")
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.SendDestroyTexts());
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.CompareTag("Hacks"))
        {
            StartCoroutine(VideoHack());
            if (collision.gameObject.name == "FlashEffect")
            {
                StartCoroutine(FlashEffect());
                StartCoroutine(LowLoopSound());
                StartCoroutine(LowHackSound());
            }
            else if (collision.gameObject.name == "camRotater")
            {
                float zRot = collision.gameObject.transform.localEulerAngles.z;
                StartCoroutine(LowLoopSound());
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rotateSound);
                Debug.Log("z rot: " + collision.gameObject.transform.localEulerAngles.z);
                Camera.main.transform.DORotate(new Vector3(0, 0, zRot), 1f).SetEase(Ease.InExpo);
                Destroy(collision.gameObject);
            }

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "GameEnd")
        {
            StartCoroutine(GameEnd());
            timeline.SetActive(true);
            timeline.GetComponent<PlayableDirector>().Play();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Spiked Ball")
        {
            enemyExplosion.SetActive(true);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.enemyDeathSound);

            enemyExplosion.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            _jumpCounter = 0;
            if (animator.GetBool("isRunning"))
            {
                _particleSystemObject.SetActive(true);
                _particleSystem.Play();
            }
            else
            {
                _particleSystemObject.SetActive(false);
                _particleSystem.Stop();
            }
        }
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(MuteLoopSound());
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.winSound);
        canMove = false;
    }

    IEnumerator FlashEffect()
    {
        LiftGammaGain liftGammaGain = VolumeManager.instance.stack.GetComponent<LiftGammaGain>();
        liftGammaGain.active = true;
        float timer = 0f;
        while (timer < 5f)
        {
            // Rastgele değerlerin atanması
            gainX = Random.Range(-1f, 1f);
            gainY = Random.Range(-1f, 1f);
            gainZ = Random.Range(-1f, 1f);

            gammaX = Random.Range(-1f, 1f);
            gammaY = Random.Range(-1f, 1f);
            gammaZ = Random.Range(-1f, 1f);

            liftX = Random.Range(-1f, 1f);
            liftY = Random.Range(-1f, 1f);
            liftZ = Random.Range(-1f, 1f);

            // Değişkenleri kullanarak liftGammaGain'in güncellenmesi
            liftGammaGain.gain = new Vector4Parameter(new Vector4(gainX, gainY, gainZ, 0), true);
            liftGammaGain.gamma = new Vector4Parameter(new Vector4(gammaX, gammaY, gammaZ, 0), true);
            liftGammaGain.lift = new Vector4Parameter(new Vector4(liftX, liftY, liftZ, 0), true);

            // Timer'ın güncellenmesi
            timer += Time.deltaTime;

            // Bir sonraki frame'e geçmek için bekleniyor
            yield return null;
        }

        yield return new WaitForSeconds(5f);
        liftGammaGain.active = false;
    }

    IEnumerator VideoHack()
    {
        videoSquareTexture.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.41f);
        videoSquareTexture.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        videoSquareTexture.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        videoSquareTexture.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        videoSquareTexture.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        videoSquareTexture.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.8f);
        videoSquareTexture.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        videoSquareTexture.GetComponent<SpriteRenderer>().enabled = false;
    }


    IEnumerator LowHackSound()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hackSound);
        yield return new WaitForSeconds(5f);
        SoundManager.Instance.StopOneShot();
    }
    IEnumerator LowLoopSound()
    {
        SoundManager.Instance.loopAudioSource.volume = 0.3f;
        yield return new WaitForSeconds(4f);
        SoundManager.Instance.loopAudioSource.volume = 1f;
    }

    IEnumerator MuteLoopSound()
    {
        SoundManager.Instance.loopAudioSource.volume = 0f;
        yield return new WaitForSeconds(1f);
    }
}