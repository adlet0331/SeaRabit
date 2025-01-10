using MapObjects;
using UnityEngine;

namespace MapReset
{
    public class ResetCollisionObject : MonoBehaviour
    {
        [SerializeField] private ControllMapReset resetObject; 
        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log("Colided");
            if (col.gameObject.layer != 6) return;
            resetObject.ResetMap();
        }
    }
}