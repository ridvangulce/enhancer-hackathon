using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

       

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool(0, true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool(0,true);
        }
        else
        {
            animator.SetBool(0, false);
        }
    }
}
