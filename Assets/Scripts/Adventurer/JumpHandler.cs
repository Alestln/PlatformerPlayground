using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Adventurer
{
    public class JumpHandler : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _jumpForce = 7f;
        [SerializeField] private float _doubleJumpForce = 5f;
        [SerializeField] private float _doubleJumpDelay = 0.2f;

        [Header("Ground Check")]
        [SerializeField] private float _groundCheckDistance = 1f;
        [SerializeField] private LayerMask _groundLayer;

        private Rigidbody2D _rigidBody;
        private bool _canDoubleJump;

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


            Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.red);
        }

        public void PerformJump()
        {
            if (IsGrounded)
            {
                ApplyJumpForce(_jumpForce);
                IsGrounded = false;

                StartCoroutine(EnableDoubleJump());
            }
            else if (_canDoubleJump)
            {
                ApplyJumpForce(_doubleJumpForce);
                _canDoubleJump = false;
            }
        }

        private void ApplyJumpForce(float jumpForce)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
        }

        private IEnumerator EnableDoubleJump()
        {
            yield return new WaitForSeconds(_doubleJumpDelay);

            _canDoubleJump = true;
        }
    }
}
