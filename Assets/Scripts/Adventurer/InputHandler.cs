using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(StateController))]
public class InputHandler : MonoBehaviour
{
    private StateController controller;

    private void Awake()
    {
        controller = GetComponent<StateController>();
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        controller.UpdateMovementDirection(horizontal);

        if (horizontal != 0)
        {
            controller.SetState(IsRunning() ? State.Running : State.Walking);
        }
        else
        {
            controller.SetState(State.Idle);
        }
    }

    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}