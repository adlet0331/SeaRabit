using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapObjects
{
    public class BounceObject : MonoBehaviour
    {
        [SerializeField] private float power;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer != 6) return;
            var rbody = collision.transform;
            rbody.GetComponent<Rigidbody2D>().AddForce(Vector2.up * power);
        }
    }
}
