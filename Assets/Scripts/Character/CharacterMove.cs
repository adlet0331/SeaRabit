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
    [Space()]
    public float raycastDistance = 0.5f;
    
    // Components
    private Rigidbody2D _rb2d;
    private SpriteRenderer _sr;
    
    // Internal Variables
    private int moveState;
    private float moveTimer;
    private int lookDirection;
    private Vector2 downDirection;

    private bool isGrounded;
    
    private Vector2 RightDirection => new Vector2(-downDirection.y, downDirection.x); // downDirection rotated +90 degrees
    
    public float Stamina { get; private set; }
    
    #region Movement Logic

    private void DetectDownDirection()
    {
        var circleOrigin = transform.right * -0.065f + transform.up * -0.0875f;
        var hitPoints =
            Physics2D.RaycastAll(transform.position + circleOrigin, -transform.up, raycastDistance, LayerMask.GetMask("Platform"));

        if (hitPoints.Length > 0)
        {
            var calculatedDown =
                hitPoints
                    .Where(hit => hit.collider.gameObject != gameObject)
                    .Aggregate(Vector2.zero, (current, hit) => current - hit.normal);
            downDirection = calculatedDown.normalized;
            isGrounded = true;
        }
        else
        {
            downDirection = Vector2.down;
            isGrounded = false;
        }
    }
    
    #endregion
    
    #region Unity Events
    
    private void Awake()
    {
        // Init Components and Variables
        
        _rb2d = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        moveState = 0;
        moveTimer = 0f;
        lookDirection = 1;
        downDirection = Vector2.down;
        
        isGrounded = false;

        Stamina = 100f;
    }

    private void Update()
    {
        DetectDownDirection();
        
        // Move Logic
        if (moveTimer > 0f)  moveTimer -= Time.deltaTime;
        else moveState = 0;

        if (Mathf.Abs(moveState) >= 3)
        {
            lookDirection = (int)Mathf.Sign(moveState);
            _rb2d.AddForce(RightDirection * lookDirection, ForceMode2D.Impulse);
            
            _sr.flipX = lookDirection < 0;
            
            moveState = 0;
        }
        
        // Limit Velocity and Rotation
        _rb2d.velocity = new Vector2(Mathf.Clamp(_rb2d.velocity.x, -maxSpeed, maxSpeed), _rb2d.velocity.y);
        _rb2d.rotation = Mathf.Clamp(_rb2d.rotation, -45f, 45f);
        
        if (!isGrounded) _rb2d.AddTorque(-_rb2d.rotation / 90f * 0.25f, ForceMode2D.Force);
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
    
    #region Debug / Gizmos Actions

    private void OnDrawGizmos()
    {
        var originPoint = transform.position + transform.right * -0.065f + transform.up * -0.0875f;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(originPoint, originPoint - transform.up * raycastDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(originPoint, originPoint + (Vector3)downDirection * raycastDistance);
    }
    
    #endregion
}
