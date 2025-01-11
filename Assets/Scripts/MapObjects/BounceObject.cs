using System;
using NonDestroyObject;
using UnityEngine;

namespace MapObjects
{
    public class BounceObject : MonoBehaviour
    {
        [SerializeField] private float power;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            MovingWhenTriggeredManger.Instance.ObjectTriggered(0);
            
            if (collision.gameObject.layer != 6) return;
            var angle = (transform.rotation.eulerAngles.z + 90.0f) * Math.PI / 180.0f; 
            var characterRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            characterRigidBody.AddForce(new Vector2(
                power * (float)Math.Cos(angle),
                power * (float)Math.Sin(angle)));
        }
    }
}
