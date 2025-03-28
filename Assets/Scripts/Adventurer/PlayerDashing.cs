using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDashing : MonoBehaviour
{
    [SerializeField] private float _dashSpeed = 15f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;

    public bool IsDashing { get; private set; }

    private Rigidbody2D _rigidBody;
    private PlayerInputHandler _input;

    private bool _canDash = true;
    private Vector2 _dashDirection;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInputHandler>();

        _input.OnDashRequested += StartDash;

    }

    private void FixedUpdate()
    {
        if (IsDashing)
        {
            _rigidBody.velocity = _dashDirection * _dashSpeed;
        }
    }

    private void OnDestroy()
    {
        _input.OnDashRequested -= StartDash;
    }

    private void StartDash()
    {
        if (_canDash && !IsDashing)
        {
            IsDashing = true;
            _canDash = false;

            _dashDirection = new Vector2(Mathf.Sign(_rigidBody.velocity.x), 0);

            Invoke(nameof(EndDash), _dashDuration);
            Invoke(nameof(ResetDash), _dashCooldown);
        }
    }

    private void EndDash()
    {
        IsDashing = false;
    }

    private void ResetDash()
    {
        _canDash = true;
    }
}
