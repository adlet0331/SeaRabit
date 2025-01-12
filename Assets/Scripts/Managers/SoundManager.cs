using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace NonDestroyObject
{
    public enum AudioClipEnum
    {
        CharacterMove1 = 0,
        CharacterMove2 = 1,
        Intro4CutManga1 = 2,
        Intro4CutManga2 = 3,
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

        private void Start()
        {
            bgmAudioSource.clip = bgmClips[0];
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }

        private void OnEnable()
        {
            if (bgmAudioSource.clip != null) 
                bgmAudioSource.Play();
        }

        public Transform GenerateAudioSourceAndPlay(Transform targetTransform, AudioClipEnum soundClipEnum)
        {
            var soundObject = new GameObject();
            soundObject.transform.position = targetTransform.position;
            AudioSource objectSource = soundObject.AddComponent<AudioSource>();
            objectSource.clip = audioClips[(int)soundClipEnum];
            objectSource.minDistance = 0.0f;
            objectSource.volume = 1.0f;
            if (soundClipEnum == AudioClipEnum.CharacterMove1) objectSource.volume = 0.3f;
            
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