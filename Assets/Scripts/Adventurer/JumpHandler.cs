using UnityEngine;

namespace Assets.Scripts.Adventurer
{
    public class JumpHandler : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _jumpForce = 7f;

        [Header("Ground Check")]
        [SerializeField] private float _groundCheckDistance = 1f;
        [SerializeField] private LayerMask _groundLayer;

        private Rigidbody2D _rigidBody;

        public bool IsGrounded { get; private set; }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            CheckGrounded();
        }

        private void CheckGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayer);
            IsGrounded = hit.collider is not null;

            Debug.Log($"IsGrounded: {IsGrounded}");

            Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.red);
        }

        public void PerformJump()
        {
            if (IsGrounded)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
                IsGrounded = false;
            }
        }
    }
}
