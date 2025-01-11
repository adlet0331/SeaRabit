using System.Collections;
using NonDestroyObject;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapObjects
{
    public class ResetCollisionObject : MonoBehaviour
    {
        [SerializeField] private bool deactivateOnPearl;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col == null) return;
            if (deactivateOnPearl && col.gameObject.layer == 9)
            {
                gameObject.SetActive(false);
                col.gameObject.SetActive(false);
                return;
            }
            if (col.gameObject.layer != 6) return;
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            GameObject.Find("Transition").GetComponent<Transition>().FadeOut();
            GameObject.FindObjectOfType<CharacterMove>().OnDeath();
            SoundManager.Instance.GenerateAudioSourceAndPlay(transform, AudioClipEnum.ResetPortal);
            yield return new WaitForSeconds(1.1f);
            SceneManager.LoadScene("TempScene");
        }
    }
}