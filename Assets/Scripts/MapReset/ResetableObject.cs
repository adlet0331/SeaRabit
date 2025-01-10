using System;
using MapObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace MapReset
{
    public class ResetableObject : MonoBehaviour, IResetable
    {
        [SerializeField] private Vector3 _initPosition;
        [SerializeField] private Quaternion _initRotation;
        private void Awake()
        {
            _initPosition = transform.position;
            _initRotation = transform.rotation;
        }

        public virtual void ResetStatus()
        {
            transform.position = _initPosition;
            transform.rotation = _initRotation;
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = Vector2.zero;
                rigidBody.angularVelocity = 0.0f;
            }
        }
    }
}