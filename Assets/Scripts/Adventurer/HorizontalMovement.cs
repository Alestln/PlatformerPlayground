using UnityEngine;

namespace Assets.Scripts.Adventurer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HorizontalMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidBody;

        private float _currentSpeed;
        private float _direction;

        [SerializeField] private float _walkSpeed = 3f;
        [SerializeField] private float _runSpeed = 6f;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _currentSpeed = _walkSpeed;
        }

        public void SetRunning(bool isRunning)
        {
            _currentSpeed = isRunning ? _runSpeed : _walkSpeed;
        }

        public void SetDirection(float direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            ApplyHorizontalMovement();
        }

        private void ApplyHorizontalMovement()
        {
            _rigidBody.velocity = new Vector2(_currentSpeed * _direction, _rigidBody.velocity.y);
        }
    }
}
