using UnityEngine;

namespace MapObjects
{
    public class GravityChangeTrigger : MonoBehaviour
    {
        [Header("Internal Values Can Be Changed")]
        [SerializeField] private float newGravityScale;
        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<Rigidbody2D>().gravityScale *= newGravityScale;
        }
    }
}
