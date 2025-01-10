using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [Header("Should Be Inited in Unity")]
    [SerializeField] private Transform followingTransform;

    private Vector3 _beforeMove;
    private void FixedUpdate()
    {
        transform.position = followingTransform.position;
    }
}
