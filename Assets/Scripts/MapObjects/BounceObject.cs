using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    [SerializeField] private float power;

    private void OnCollisionEnter(Collision collision)
    {
        var rbody = collision.transform;
        rbody.GetComponent<Rigidbody2D>().AddForce(Vector2.up * power);
    }
}
