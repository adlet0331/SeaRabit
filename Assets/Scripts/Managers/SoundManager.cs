using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace NonDestroyObject
{
    public enum AudioClipEnum
    {
        Intro4CutManga1 = 0,
        Intro4CutManga2 = 1,
        CharacterMove1 = 2,
        CharacterMove2 = 3,
        ResetPortal = 4,
        Bubble = 5
    }

    public enum Bgm
    {
        bgm1 = 0,
        bgm2 = 1
    }
    
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] private Bgm currentBgm;
        [SerializeField] private AudioClip[] bgmClips;
        [SerializeField] private AudioSource bgmAudioSource;

        private void Awake()
        {
            bgmAudioSource.clip = bgmClips[0];
        }

        private void OnEnable()
        {
            bgmAudioSource.Play();
        }

        public Transform GenerateAudioSourceAndPlay(Transform targetTransform, AudioClipEnum soundClipEnum)
        {
            var soundObject = new GameObject();
            soundObject.transform.position = targetTransform.position;
            AudioSource objectSource = soundObject.AddComponent<AudioSource>();
            objectSource.clip = audioClips[(int)soundClipEnum];
            
            objectSource.Play();
            StartCoroutine(DestroyAfterTime(soundObject.gameObject, objectSource.clip.length));

            return soundObject.transform;
        }
        
        IEnumerator DestroyAfterTime(GameObject willbeDestroyedObject, float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(willbeDestroyedObject);
        }

        public void SwitchBGM(Bgm bgm)
        {
            currentBgm = bgm;
            bgmAudioSource.clip = bgmClips[(int)bgm];
            bgmAudioSource.time = 0.0f;
            bgmAudioSource.Play();
        }
    }
}