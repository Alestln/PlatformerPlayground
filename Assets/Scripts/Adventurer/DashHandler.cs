using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DashHandler : MonoBehaviour
{
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;

    private Rigidbody2D _rigidBody;
    private bool _canDash = true;
    private float _originalGravityScale;

    public bool IsDashing { get; private set; }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _originalGravityScale = _rigidBody.gravityScale;
    }

    public void PerformDash()
    {
        if (_canDash)
        {
            float direction = Mathf.Sign(_rigidBody.transform.localScale.x);
            StartCoroutine(ApplyDash(direction));
        }
    }

    private IEnumerator ApplyDash(float direction)
    {
        IsDashing = true;
        _canDash = false;
        _rigidBody.gravityScale = 0f;

        _rigidBody.velocity = new Vector2(_dashSpeed * direction, 0f);

        yield return new WaitForSeconds(_dashDuration);

        IsDashing = false;
        _rigidBody.gravityScale = _originalGravityScale;

        yield return new WaitForSeconds(_dashCooldown);

        _canDash = true;
    }
}