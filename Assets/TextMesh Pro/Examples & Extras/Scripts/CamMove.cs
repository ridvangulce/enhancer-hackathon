using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed;
    private PlayerController _playerController;
    public GameObject gameOverCanvas;

    void Start()
    {
        _playerController = target.transform.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_playerController != null)
        {
            if (!_playerController.isOver)
            {
                transform.position = Vector3.Slerp(transform.position,
                    new Vector3(target.position.x, target.position.y, 0),
                    cameraSpeed);
            }
            else
            {
                gameOverCanvas.SetActive(true);
            }
        }
    }
}