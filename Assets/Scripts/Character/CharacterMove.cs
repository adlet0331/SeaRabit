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
    private Vector2 ceilingDirection;

    private bool isGrounded;
    private bool ceilingHoldable;
    private bool ceilingHolding;

    private bool upPressed;
    private bool spacePressed;
    
    private Vector2 RightDirection => new Vector2(-downDirection.y, downDirection.x); // downDirection rotated +90 degrees
    
    public StaminaSystem staminaSystem;
    
    #region Movement Logic

    private void DetectDownDirection()
    {
        var pointOrigin = transform.right * -0.065f + transform.up * -0.0875f;
        var hitPoints = Physics2D.RaycastAll(
            transform.position + pointOrigin,
            -transform.up,
            raycastDistance,
            LayerMask.GetMask("Platform"))
            ;

        if (hitPoints.Length > 0)
        {
            var calculatedDown =
                hitPoints
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
    private void DetectUpDirection()
    {
        if (!upPressed && !spacePressed)
        {
            ceilingHoldable = false;
            return;
        }
        
        var hitPoints = Physics2D.RaycastAll(
            transform.position,
            transform.up,
            raycastDistance,
            LayerMask.GetMask("Platform"))
            ;

        if (ceilingHolding)
        {
            ceilingHolding = hitPoints.Length > 0;
            ceilingDirection = ceilingHolding ? -hitPoints[0].normal : Vector2.up;
            return;
        }

        ceilingHoldable = hitPoints.Length > 0;

        ceilingDirection = ceilingHoldable ? -hitPoints[0].normal : Vector2.zero;
    }

    private void MovementNormal(bool stamina = false)
    {
        if (Mathf.Abs(moveState) < 3) return;
        
        lookDirection = (int)Mathf.Sign(moveState);

        var forceDirection = RightDirection * lookDirection;
        if (!stamina && forceDirection.y >= Mathf.Sqrt(0.5f)) return; // Prevent Uphill when not using stamina
        else _rb2d.AddForce(forceDirection, ForceMode2D.Impulse);
            
        _sr.flipX = lookDirection < 0;
            
        moveState = 0;
    }
    private void MovementCeiling(bool stamina = true)
    {
        if (Mathf.Abs(moveState) < 3) return;
        
        lookDirection = (int)Mathf.Sign(moveState);

        var forceDirection = RightDirection * lookDirection;
        _rb2d.AddForce(forceDirection, ForceMode2D.Impulse);
            
        _sr.flipX = lookDirection < 0;
            
        moveState = 0;
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
        ceilingHoldable = false;
        ceilingHolding = false;

        staminaSystem = GetComponent<StaminaSystem>();
    }

    private void Update()
    {
        DetectDownDirection();
        DetectUpDirection();
        
        // Move Logic
        if (moveTimer > 0f)  moveTimer -= Time.deltaTime;
        else moveState = 0;

        if (ceilingHolding && staminaSystem.CanUseStamina)
        {
            MovementCeiling();
            _rb2d.AddForce((Vector2.down - ceilingDirection.normalized) * (Physics2D.gravity.y * _rb2d.gravityScale * 0.125f), ForceMode2D.Force);
        }
        else MovementNormal(spacePressed && staminaSystem.CanUseStamina);
        
        // Limit Velocity and Rotation
        _rb2d.velocity = new Vector2(Mathf.Clamp(_rb2d.velocity.x, -maxSpeed, maxSpeed), _rb2d.velocity.y);
        _rb2d.rotation = Mathf.Clamp(_rb2d.rotation, -45f, 45f);
        
        if (!isGrounded) _rb2d.AddTorque(-_rb2d.rotation / 90f * 0.25f, ForceMode2D.Force);
    }
    
    #endregion
    
    #region Input Actions
    
    private void OnKeyW(InputValue value)
    {
        upPressed = value.Get<float>() > 0;
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
        spacePressed = value.Get<float>() > 0;
        staminaSystem.tryUseStamina = spacePressed;
        if (spacePressed && ceilingHoldable)
        {
            ceilingHolding = true;
        }
        if (!value.isPressed)
        {
            ceilingHolding = false;
        }
    }
    
    #endregion
    
    #region Debug / Gizmos Actions

    private void OnDrawGizmos()
    {
        var originPoint = transform.position + transform.right * -0.065f + transform.up * -0.0875f;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(originPoint, originPoint - transform.up * raycastDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * raycastDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(originPoint, originPoint + (Vector3)downDirection * raycastDistance);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)ceilingDirection * raycastDistance);
    }
    
    #endregion
}
