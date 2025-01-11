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
            SoundManager.Instance.GenerateAudioSourceAndPlay(transform, AudioClipEnum.ResetPortal);
            SceneManager.LoadScene("TempScene");
        }
    }
}