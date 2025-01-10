using System;
using UnityEngine;

namespace MapObjects
{
    public class GravityChangeTrigger : MonoBehaviour
    {
        [Header("Internal Values Can Be Changed")]
        [SerializeField] private float newGravityScale;
        [SerializeField] private float originalGravityScale;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != 6) return;
            originalGravityScale = other.GetComponent<Rigidbody2D>().gravityScale;
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.layer != 6) return;
            other.GetComponent<Rigidbody2D>().gravityScale = newGravityScale;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer != 6) return;
            other.GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
        }
    }
}
