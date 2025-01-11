using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != 6) return;
        StartCoroutine(ClearCoroutine());
    }

    private IEnumerator ClearCoroutine()
    {
        GameObject.Find("Transition").GetComponent<Transition>().FadeClear();
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("FinishScene");
    }
}
