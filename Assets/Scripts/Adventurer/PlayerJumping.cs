using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private float _jumpSpeed = 10f;
    [SerializeField] private float _doubleJumpSpeed = 8f;
    [SerializeField] private float _groundCheckDistance = 1f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidBody;
    private PlayerInputHandler _input;
    private bool _isGrounded;
    private bool _canDoubleJump;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        CheckGrounded();

        if (_input.JumpPressed)
        {
            if (_isGrounded)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpSpeed);
                _canDoubleJump = true;
            }
            else if (_canDoubleJump)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _doubleJumpSpeed);
                _canDoubleJump = false;
            }

            _input.ResetJump();
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayer);
        _isGrounded = hit.collider is not null;

        Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.red);
    }
}
