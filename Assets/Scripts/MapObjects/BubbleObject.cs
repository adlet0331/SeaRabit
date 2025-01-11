using System.Collections;
using NonDestroyObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapObjects
{
    public class BubbleObject : MonoBehaviour
    {
        [SerializeField] private Sprite[] variousSprites;
        
        [SerializeField] private float maxLifeTime = 6.0f;
        [SerializeField] private float minLifeTime = 3.0f;
        
        [SerializeField] private float maxVelocity = 6.0f;
        [SerializeField] private float minVelocity = 3.0f;
        private float velocity;
        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = variousSprites[Random.Range(0, variousSprites.Length)];
            transform.localScale = Vector3.one * Random.Range(0.5f, 1.0f);
            velocity = Random.Range(minVelocity, maxVelocity);
            StartCoroutine(SoundSourceTrackingMeAndDestroy(Random.Range(minLifeTime, maxLifeTime)));
        }

        IEnumerator SoundSourceTrackingMeAndDestroy(float time)
        {
            for (int i = 0; i < time / Time.fixedDeltaTime; i++)
            {
                transform.position += Vector3.up * (velocity * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
            Destroy(gameObject);
        }
    }
}