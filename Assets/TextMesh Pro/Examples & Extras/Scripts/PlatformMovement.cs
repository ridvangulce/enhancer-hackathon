using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _duration;
    void Start()
    {
        transform.DOMoveX(_distance, _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}