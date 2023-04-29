using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Player movement speed
    [SerializeField] private float jumpForce = 8f; // Jump force
    [SerializeField] private Transform groundCheck; // Transform object to check if the player is grounded
    [SerializeField] private LayerMask groundLayer; // Layer mask for the ground objects
    public bool isGrounded, canJump; // Boolean to check if the player is grounded
    private float groundCheckRadius = 0.1f; // Radius of the ground check sphere
    float gainX, gainY, gainZ, gammaX, gammaY, gammaZ, liftX, liftY, liftZ;
    public Rigidbody2D rb;
    Animator animator;
    public Transform videoSquareTexture;
    public GameObject enemyExplosion, playerExplosion;
    private int _jumpCounter;
    [HideInInspector] public bool isOver;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        }

        // Move player horizontally
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        // Jump when the player presses the spacebar and is grounded
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCounter <= 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _jumpCounter++;
        }

        // Check if the player is in the air and disable jumping until they land on the ground again
        if (!isGrounded)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LimitLine"))
        {
            isOver = true;
            Debug.Log("is Over");
        }

        if (collision.gameObject.CompareTag("destroyText"))
        {
            playerExplosion.SetActive(true);
            playerExplosion.transform.position = transform.position;
            GameManager.Instance.StartCoroutine(GameManager.Instance.RestartGame());
            Destroy(gameObject);
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
            }
            else if (collision.gameObject.name == "camRotater")
            {
                float zRot = collision.gameObject.transform.localEulerAngles.z;
                Debug.Log("z rot: " + collision.gameObject.transform.localEulerAngles.z);
                Camera.main.transform.DORotate(new Vector3(0, 0, zRot), 1f).SetEase(Ease.InExpo);
                Destroy(collision.gameObject);
            }

            Destroy(collision.gameObject);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Spiked Ball")
        {
            enemyExplosion.SetActive(true);
            enemyExplosion.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            _jumpCounter = 0;
        }
    }
}