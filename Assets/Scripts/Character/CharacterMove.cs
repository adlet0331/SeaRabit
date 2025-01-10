using System.Collections;
using System.Collections.Generic;
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
    
    private Vector2 leftDirection => downDirection.
    
    public float Stamina { get; private set; }
    
    
    #region Unity Events
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        moveState = 0;
        moveTimer = 0f;
        downDirection = Vector2.down;

        Stamina = 100f;
    }

    private void Update()
    {
        if (moveTimer > 0f)  moveTimer -= Time.deltaTime;
        else moveState = 0;

        if (Mathf.Abs(moveState) >= 3)
        {
            _rb2d.AddForce(new Vector2(moveSpeed * Mathf.Sign(moveState), 0f), ForceMode2D.Impulse);
            
            moveState = 0;
        }
        
        _rb2d.velocity = new Vector2(Mathf.Clamp(_rb2d.velocity.x, -maxSpeed, maxSpeed), _rb2d.velocity.y);
        
        if (Mathf.Abs(_rb2d.velocity.x) >= 0.01f) _sr.flipX = _rb2d.velocity.x < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        downDirection = -collision.transform.up;
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
    
    
}
