using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    
    //Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Keyboard.current == null)
        {
            _moveInput = Vector2.zero;
            return;
        }
        
        Vector2 input = Vector2.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            input.y += 1f;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            input.y -= 1f;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            input.x -= 1f;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            input.x += 1f;
        }
        _moveInput = input.normalized;
    }
    
    void FixedUpdate()
    {
        _rb.linearVelocity = baseSpeed * _moveInput;
    }
    
    private void OnDisable()
    {
        if (_rb != null)
            _rb.linearVelocity = Vector2.zero;
    }
}
