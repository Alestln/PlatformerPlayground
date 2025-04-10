using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _runSpeed = 6f;

    private Rigidbody2D _rigidbody;
    private float _currentDirection = 0f;
    private float _selectedSpeed = 0f;
    private bool _isMovementEnabled = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isMovementEnabled)
        {
            _rigidbody.velocity = new Vector2(_currentDirection * _selectedSpeed, _rigidbody.velocity.y);
        }
    }

    public void Move(float direction, bool isRunning)
    {
        _currentDirection = direction;
        _selectedSpeed = isRunning ? _runSpeed : _walkSpeed;
    }

    public void StopMovement()
    {
        _currentDirection = 0f;
        _selectedSpeed = 0f;
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    public void EnableMovement(bool enable)
    {
        _isMovementEnabled = enable;
        if (!enable)
        {
            StopMovement();
        }
    }
}