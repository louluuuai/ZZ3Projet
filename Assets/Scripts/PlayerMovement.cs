using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Keyboard.current == null)
        {
            _moveInput = Vector2.zero;
            return;
        }
        
        Vector2 input = Vector2.zero;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            input.y += 1f;
        }

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            input.y -= 1f;
        }

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            input.x -= 1f;
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) 
        {
            input.x += 1f;
        }
        _moveInput = input.normalized;
    }
    
    private void FixedUpdate()
    {
        _rb.linearVelocity = baseSpeed * _moveInput;
    }
    
    private void OnDisable()
    {
        if (_rb != null)
            _rb.linearVelocity = Vector2.zero;
    }
}
