using System;
using System.Collections;
using MapReset;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace MapObjects
{
    [Serializable]
    public struct MoveTargetAndTime
    {
        public Transform tTransform;
        public float time;
    }
    public class MovingObjectWhenTriggered : ResetableObject
    {
        [SerializeField] private MoveTargetAndTime[] TargetAndTimes;
        private bool isTriggered;
        private Coroutine activatedCoroutine;

        public override void ResetStatus()
        {
            base.ResetStatus();
            if (activatedCoroutine != null)
            {
                StopCoroutine(activatedCoroutine);
            }
            gameObject.SetActive(true);
            isTriggered = false;
        }

        private void Start()
        {
            isTriggered = false;
        }

        public void MoveTriggered()
        {
            if (isTriggered) return;
            isTriggered = true;
            activatedCoroutine = StartCoroutine(StartMove(0));
        }
        
        IEnumerator StartMove(int index)
        {
            var target = TargetAndTimes[index];
            var moveDirection = target.tTransform.position - transform.position;
            var moveInterval = moveDirection / target.time * Time.fixedDeltaTime;
            
            var facing = Math.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.Rotate(new Vector3(0, 0, (float)facing) - transform.rotation.eulerAngles);

            for (int i = 0; i < target.time / Time.fixedDeltaTime; i++)
            {
                transform.position += moveInterval;
                yield return new WaitForFixedUpdate();
            }

            if (TargetAndTimes.Length > index + 1)
            {
                activatedCoroutine = StartCoroutine(StartMove(index + 1));
            }
            else
            {
                gameObject.SetActive(false);
                activatedCoroutine = null;
            }
            yield return null;
        }
    }
}