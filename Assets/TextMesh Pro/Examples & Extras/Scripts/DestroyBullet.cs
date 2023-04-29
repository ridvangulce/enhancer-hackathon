using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyBullet : MonoBehaviour
{
    public Transform target;
    private Vector3 targetTransform;
    public float speed = 0.01f;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        targetTransform = target.position;
    }

    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(targetTransform.x, targetTransform.y, 10), speed);

    }
}
