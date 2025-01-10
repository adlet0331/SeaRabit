using UnityEngine;

namespace MapObjects
{
    public class ChangeGravityScale : MonoBehaviour
    {
        [Header("Internal Values Can Be Changed")]
        [SerializeField] private float newGravityScale;
        private void OnCollisionEnter2D(Collision2D other)
        {
            var rbody = other.transform.GetComponent<Rigidbody2D>();
            rbody.gravityScale = newGravityScale;
        }
    }
}