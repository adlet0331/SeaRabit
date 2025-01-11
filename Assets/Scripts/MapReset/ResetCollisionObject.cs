using NonDestroyObject;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapReset
{
    public class ResetCollisionObject : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.layer == 9)
            {
                gameObject.SetActive(false);
                col.gameObject.SetActive(false);
            }
            if (col.gameObject.layer != 6) return;
            SceneManager.LoadScene("MainScene");
            SoundManager.Instance.GenerateAudioSourceAndPlay(transform, AudioClipEnum.ResetPortal);
        }
    }
}