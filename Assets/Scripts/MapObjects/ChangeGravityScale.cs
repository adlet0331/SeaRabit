using UnityEngine;

namespace MapObjects
{
    public class ChangeGravityScale : MonoBehaviour
    {
        [SerializeField] private float newGravityScale;
        private void OnCollisionEnter2D(Collider other)
        {
            var rbody = other.GetComponent<Rigidbody2D>();
            rbody.gravityScale = newGravityScale;
        }
    }
}