using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyBullet : MonoBehaviour
{
    public Transform target;
    public float speed = 0.01f;

    private void Start()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void Update()
    {
        target = GameObject.Find("Player").transform;
        transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x, target.position.y, 10), speed);

    }
}
