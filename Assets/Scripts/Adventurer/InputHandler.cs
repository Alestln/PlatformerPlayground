using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(StateController))]
public class InputHandler : MonoBehaviour
{
    private StateController _controller;

    private void Awake()
    {
        _controller = GetComponent<StateController>();
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        _controller.UpdateMovementDirection(horizontal);

        if (horizontal != 0)
        {
            _controller.SetState(IsRunning() ? State.Running : State.Walking);
        }
        else
        {
            _controller.SetState(State.Idle);
        }
    }

    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}