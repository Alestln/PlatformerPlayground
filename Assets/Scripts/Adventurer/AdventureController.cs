using Assets.Scripts.Adventurer;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private CharacterFlipper _characterFlipper;

    [SerializeField] private HorizontalMovementHandler _horizontalMovementHandler;
    [SerializeField] private AnimationHandler _animationHandler;
    [SerializeField] private JumpHandler _jumpHandler;
    [SerializeField] private DashHandler _dashHandler;

    private CharacterState _currentState = CharacterState.Idle;
    private InputData _inputData;

    private void Update()
    {
        _animationHandler.SetGrounded(_jumpHandler.IsGrounded);

        _inputData = _inputHandler.CurrentInput;

        if (_currentState != CharacterState.Dash)
        {
            _characterFlipper.UpdateDirection(_inputData.HorizontalInput);
        }

        _currentState = DetermineState(_inputData);

        UpdateStateAndCommand();
    }

    private CharacterState DetermineState(InputData input)
    {
        if (_dashHandler.IsDashing)
        {
            return CharacterState.Dash;
        }

        if (input.IsDashing)
        {
            input.IsDashing = false;
            return CharacterState.Dash;
        }

        if (input.IsJumping)
        {
            input.IsJumping = false;
            return CharacterState.Jump;
        }

        if (!Mathf.Approximately(input.HorizontalInput, 0f))
        {
            return input.IsRunHeld ? CharacterState.Running : CharacterState.Walking;
        }
        else
        {
            return CharacterState.Idle;
        }
    }

    private void UpdateStateAndCommand()
    {
        _animationHandler.UpdateStateAnimation(_currentState);

        switch (_currentState)
        {
            case CharacterState.Idle:
                _horizontalMovementHandler.EnableMovement(true);
                _horizontalMovementHandler.StopMovement();
                break;

            case CharacterState.Walking:
                _horizontalMovementHandler.EnableMovement(true);
                _horizontalMovementHandler.Move(_inputData.HorizontalInput, false);
                break;

            case CharacterState.Running:
                _horizontalMovementHandler.EnableMovement(true);
                _horizontalMovementHandler.Move(_inputData.HorizontalInput, true);
                break;

            case CharacterState.Jump:
                _horizontalMovementHandler.EnableMovement(true);
                _jumpHandler.PerformJump();
                _animationHandler.AnimateJump();
                break;

            case CharacterState.Dash:
                _horizontalMovementHandler.EnableMovement(false);
                _dashHandler.PerformDash();
                break;
        }
    }
}