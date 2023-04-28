using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject _loadingText;
    [SerializeField] private float _timer;

    void Start()
    {
        _loadingText.SetActive(false);
    }

    // Update is called once per frame


    public IEnumerator Timer()
    {
        animator.SetBool("isStart", true);
        
        yield return new WaitForSeconds(_timer);
        _loadingText.SetActive(true);
    }
}