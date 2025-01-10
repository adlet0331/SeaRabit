using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanCurrents : MonoBehaviour
{
    [SerializeField] private float power;
    private void OnTriggerStay2D(Collider2D other)
    {
        var characterRigidBody = other.GetComponent<Rigidbody2D>();
        characterRigidBody.AddForce(new Vector2(power/Time.deltaTime, power/Time.deltaTime));
    }
}
