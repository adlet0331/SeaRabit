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
    
    // Internal Variables
    private int _moveState = 0;
    private float _moveTimer = 0f;
    
    #region Unity Events
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_moveTimer > 0f)  _moveTimer -= Time.deltaTime;
        else _moveState = 0;

        if (Mathf.Abs(_moveState) >= 3)
        {
            // _rb2d.velocity = new Vector2(moveSpeed * Mathf.Sign(_moveState), _rb2d.velocity.y);
            _rb2d.AddForce(new Vector2(moveSpeed * Mathf.Sign(_moveState), 0f), ForceMode2D.Impulse);
            
            _moveState = 0;
        }
        
        _rb2d.velocity = new Vector2(Mathf.Clamp(_rb2d.velocity.x, -maxSpeed, maxSpeed), _rb2d.velocity.y);
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
        
        switch (_moveState)
        {
            case 0:
                _moveState = 1;
                _moveTimer = moveBufferTime;
                break;
            case -2:
                _moveState = -3;
                _moveTimer = moveBufferTime;
                break;
        }
    }

    private void OnKeyS(InputValue value)
    {
        if (!value.isPressed) return;

        switch (_moveState)
        {
            case 1:
                _moveState = 2;
                _moveTimer = moveBufferTime;
                break;
            case -1:
                _moveState = -2;
                _moveTimer = moveBufferTime;
                break;
        }
    }

    private void OnKeyD(InputValue value)
    {
        if (!value.isPressed) return;

        switch (_moveState)
        {
            case 2:
                _moveState = 3;
                _moveTimer = moveBufferTime;
                break;
            case 0:
                _moveState = -1;
                _moveTimer = moveBufferTime;
                break;
        }
    }

    private void OnKeySpace(InputValue value)
    {
        if (!value.isPressed) return;
    }
    
    #endregion
    
    
}
