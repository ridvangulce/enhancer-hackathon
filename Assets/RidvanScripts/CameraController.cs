using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private float _timer;

    void Start()
    {
        _videoPlayer.Stop();
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

    public IEnumerator Timer()
    {
        animator.SetBool("isStart", true);

        _videoPlayer.Play();
        yield return new WaitForSeconds(_timer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}