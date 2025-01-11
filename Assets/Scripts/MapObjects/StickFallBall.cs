using NonDestroyObject;
using UnityEngine;

public class StickFallBall : MonoBehaviour
{
    [SerializeField] private int triggerInt;
    [SerializeField] private Vector2 initDirection;
    
    private bool _isActivated = false;
    private void Start()
    {
        gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_isActivated) return;
        
        if (col == null) return;
        if (col.gameObject.layer != 6) return;
        if (triggerInt >= 0) MovingWhenTriggeredManger.Instance.ObjectTriggered(triggerInt);
        
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 1;
        rb2d.velocity = initDirection;
        _isActivated = true;
    }
}
