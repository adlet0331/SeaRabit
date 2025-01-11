using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TitleSceneController : MonoBehaviour
    {
        [SerializeField] private Transform howToPlayImage;
        public void StartGameButton()
        {
            SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        }

        public void ShowHowToPlay(bool isShow)
        {
            howToPlayImage.gameObject.SetActive(isShow);
        }
        
        
    }
}