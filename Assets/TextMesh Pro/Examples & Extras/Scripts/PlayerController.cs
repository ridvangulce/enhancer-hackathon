using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;          // Player movement speed
    [SerializeField] private float jumpForce = 8f;          // Jump force
    [SerializeField] private Transform groundCheck;        // Transform object to check if the player is grounded
    [SerializeField] private LayerMask groundLayer;         // Layer mask for the ground objects
    public bool isGrounded, canJump;                        // Boolean to check if the player is grounded
    private float groundCheckRadius = 0.1f;                 // Radius of the ground check sphere
    public Rigidbody2D rb;
    Animator animator;
    public Vector2 movement;
    public Transform camHolder;
    public GameObject enemyExplosion, playerExplosion;
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
    }

    private void FixedUpdate()
    {
        // Move player horizontally
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump when the player presses the spacebar and is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
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
        if (collision.gameObject.name == "camRotater")
        {
            Camera.main.transform.DORotate(new Vector3(0, 0, 180), 1f).SetEase(Ease.InExpo);
            Destroy(collision.gameObject);
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
            Debug.Log("çarpýþþþþþþtýmmm");
            GameManager.Instance.StartCoroutine(GameManager.Instance.SendDestroyTexts());
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Spiked Ball")
        {
            enemyExplosion.SetActive(true);
            enemyExplosion.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
        }
    }
}