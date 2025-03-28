using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    private Rigidbody2D _rigidBody;
    private PlayerInputHandler _input;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInputHandler>();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(_input.Horizontal * _movementSpeed, _rigidBody.velocity.y);

        _rigidBody.velocity = velocity;
    }
}
