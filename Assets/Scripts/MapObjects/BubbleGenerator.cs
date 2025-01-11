using System.Collections;
using NonDestroyObject;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MapObjects
{
    public class BubbleGenerator : MonoBehaviour
    {
        [SerializeField] private BubbleObject bubble;
        [FormerlySerializedAs("generatePosition")] [SerializeField] private Transform generateTransform;
        [SerializeField] private float bubbleXRange = 1.5f;
        [SerializeField] private float bubbleInterval = 10.0f;
        [SerializeField] private int minNumPerInterval = 3;
        [SerializeField] private int maxNumPerInterval = 6;

        private void Start()
        {
            StartCoroutine(StartBubbleGenerate());
        }

        IEnumerator StartBubbleGenerate()
        {
            SoundManager.Instance.GenerateAudioSourceAndPlay(transform, AudioClipEnum.Bubble);
            var generateCount = (int)Random.Range(minNumPerInterval, maxNumPerInterval);
            for (int i = 0; i < generateCount; i++)
            {
                var bubbleObject = Instantiate(bubble, generateTransform);
                bubbleObject.transform.position += Vector3.right * (Random.Range(-1.0f, 1.0f) * bubbleXRange);
                yield return new WaitForSeconds(bubbleInterval / (float)generateCount);
            }

            StartCoroutine(StartBubbleGenerate());
            yield return null;
        }
    }
}