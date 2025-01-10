using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    // Public Variables
    public float moveSpeed = 5f;
    public float maxSpeed = 3f;
    public float moveBufferTime = 0.05f;
    
    // Components
    private Rigidbody2D _rb2d;
    private SpriteRenderer _sr;
    
    // Internal Variables
    private int moveState;
    private float moveTimer;
    private Vector2 downDirection;
    
    private Vector2 RightDirection => new Vector2(-downDirection.y, downDirection.x);
    
    public float Stamina { get; private set; }
    
    
    #region Unity Events
    
    private void Awake()
    {
        // Init Components and Variables
        
        _rb2d = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        moveState = 0;
        moveTimer = 0f;
        downDirection = Vector2.down;

        Stamina = 100f;
    }

    private void Update()
    {
        // Detect down direction Logic
        var circleOrigin = transform.right * -0.065f + transform.up * -0.0875f;
        // var hitPoints = Physics2D.CircleCastAll(transform.position + circleOrigin, 0.05f, Vector2.down);
        var hitPoints = Physics2D.RaycastAll(transform.position + circleOrigin, -transform.up, 0.1f);

        if (hitPoints.Length > 0)
        {
            var calculatedDown =
                hitPoints
                .Where(hit => hit.collider.gameObject != gameObject)
                .Aggregate(Vector2.zero, (current, hit) => current - hit.normal);
            //hitPoints.Aggregate(Vector2.zero, (current, hit) => current - hit.normal);
            downDirection = calculatedDown.normalized;
        }
        else downDirection = Vector2.down;
        
        // Move Logic
        if (moveTimer > 0f)  moveTimer -= Time.deltaTime;
        else moveState = 0;

        if (Mathf.Abs(moveState) >= 3)
        {
            var moveDirection = Mathf.Sign(moveState);
            _rb2d.AddForce(RightDirection * moveDirection, ForceMode2D.Impulse);
            
            _sr.flipX = moveDirection < 0;
            
            moveState = 0;
        }
        
        // Limit Velocity and Rotation
        _rb2d.velocity = new Vector2(Mathf.Clamp(_rb2d.velocity.x, -maxSpeed, maxSpeed), _rb2d.velocity.y);
        _rb2d.rotation = Mathf.Clamp(_rb2d.rotation, -45f, 45f);
    }
    
    #endregion
    
    #region Input Actions
    
    private void OnKeyW(InputValue value)
    {
        if (!value.isPressed) return;
    }

    private void OnKeyA(InputValue value)
    {
        if (!value.isPressed) return;
        
        switch (moveState)
        {
            case 0:
                moveState = 1;
                moveTimer = moveBufferTime;
                break;
            case -2:
                moveState = -3;
                moveTimer = moveBufferTime;
                break;
        }
    }

    private void OnKeyS(InputValue value)
    {
        if (!value.isPressed) return;

        switch (moveState)
        {
            case 1:
                moveState = 2;
                moveTimer = moveBufferTime;
                break;
            case -1:
                moveState = -2;
                moveTimer = moveBufferTime;
                break;
        }
    }

    private void OnKeyD(InputValue value)
    {
        if (!value.isPressed) return;

        switch (moveState)
        {
            case 2:
                moveState = 3;
                moveTimer = moveBufferTime;
                break;
            case 0:
                moveState = -1;
                moveTimer = moveBufferTime;
                break;
        }
    }

    private void OnKeySpace(InputValue value)
    {
        if (!value.isPressed) return;
    }
    
    #endregion
    
    #region Debug Actions

    private void OnDrawGizmos()
    {
        var originPoint = transform.position + transform.right * -0.065f + transform.up * -0.0875f;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(originPoint, originPoint - transform.up * 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(originPoint, originPoint + (Vector3)downDirection * 0.1f);
    }
    
    #endregion
}
