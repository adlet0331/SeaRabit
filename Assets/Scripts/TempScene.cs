using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempScene : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        SceneManager.LoadScene("MainScene");
    }
}
