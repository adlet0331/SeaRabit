using System;
using System.Collections;
using UnityEngine;

namespace MapObjects
{
    [Serializable]
    public struct MoveTargetAndTime
    {
        public Transform tTransform;
        public float time;
    }
    public class MovingObjectWhenTriggered : MonoBehaviour
    {
        [SerializeField] private MoveTargetAndTime[] TargetAndTimes;
        [SerializeField] private Transform headMovingTransform;
        private bool isTriggered;
        private Coroutine activatedCoroutine;

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
            var headMoveDirection = target.tTransform.position - headMovingTransform.position;
            var headMoveInterval = headMoveDirection / target.time * Time.fixedDeltaTime;
            
            var facing = Math.Atan2(headMoveDirection.y, headMoveDirection.x) * Mathf.Rad2Deg;
            headMovingTransform.Rotate(new Vector3(0, 0, (float)facing) - headMovingTransform.rotation.eulerAngles);

            for (int i = 0; i < target.time / Time.fixedDeltaTime; i++)
            {
                headMovingTransform.position += headMoveInterval;
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

        private void OnDestroy()
        {
            if (activatedCoroutine != null) StopCoroutine(activatedCoroutine);
        }
    }
}