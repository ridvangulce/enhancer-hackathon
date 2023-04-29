using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed;
    private PlayerController _playerController;
    public GameObject gameOverCanvas;
    public GameObject finishCanvas;

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
                transform.position = Vector3.Slerp(transform.position,
                    new Vector3(target.position.x, transform.position.y, 0),
                    cameraSpeed);
                finishCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);
            }
        }
    }
}