using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanCurrents : MonoBehaviour
{
    [SerializeField] private float power;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == null) return;
        var angle = transform.rotation.eulerAngles.z * Math.PI / 180.0f; 
        var characterRigidBody = other.GetComponent<Rigidbody2D>();
        characterRigidBody.AddForce(new Vector2(
            (power * Time.deltaTime) * (float)Math.Cos(angle),
            (power * Time.deltaTime) * (float)Math.Sin(angle)));
    }
}
