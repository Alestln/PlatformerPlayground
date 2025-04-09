using Assets.Scripts.Adventurer;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private HorizontalMovement _horizontalMovement;

    public State CurrentState { get; private set; }

    private void Awake()
    {
        _horizontalMovement = GetComponent<HorizontalMovement>();
    }

    private void Start()
    {
        SetState(State.Idle);
    }

    public void SetState(State newState)
    {
        if (CurrentState == newState)
        {
            return;
        }

        CurrentState = newState;
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        switch (CurrentState)
        {
            case State.Idle:
                _horizontalMovement.SetRunning(false);
                _horizontalMovement.SetDirection(0);
                SetMovementEnabled(true);
                break;

            case State.Walking:
                _horizontalMovement.SetRunning(false);
                SetMovementEnabled(true);
                break;

            case State.Running:
                _horizontalMovement.SetRunning(true);
                SetMovementEnabled(true);
                break;
        }
    }

    private void SetMovementEnabled(bool isEnabled)
    {
        _horizontalMovement.enabled = isEnabled;
    }

    public void UpdateMovementDirection(float direction)
    {
        _horizontalMovement.SetDirection(direction);
    }
}
