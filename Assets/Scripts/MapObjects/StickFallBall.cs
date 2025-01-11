using NonDestroyObject;
using UnityEngine;

public class StickFallBall : MonoBehaviour
{
    [SerializeField] private int triggerInt;
    private void Start()
    {
        gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer != 6) return;
        if (triggerInt >= 0) MovingWhenTriggeredManger.Instance.ObjectTriggered(triggerInt);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
