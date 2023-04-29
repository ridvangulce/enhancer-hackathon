using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed;

    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x, 1, 0), cameraSpeed);
    }
}
