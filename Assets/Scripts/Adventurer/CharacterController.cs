using Assets.Scripts.Adventurer;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private HorizontalMovement _horizontalMovement;
    [SerializeField] private AnimationHandler _animationHandler;
    [SerializeField] private JumpHandler _jumpHandler;

    private CharacterState _currentState = CharacterState.Idle;
    private InputData _inputData;

    private void Update()
    {
        _inputData = _inputHandler.CurrentInput;

        CharacterState newState = DetermineState(_inputData);

        UpdateStateAndCommand(newState);

        _animationHandler.SetGrounded(_jumpHandler.IsGrounded);
    }

    private CharacterState DetermineState(InputData input)
    {
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

    private void UpdateStateAndCommand(CharacterState newState)
    {
        _currentState = newState;

        _animationHandler.UpdateStateAnimation(_currentState);

        switch (_currentState)
        {
            case CharacterState.Idle:
                _horizontalMovement.EnableMovement(true);
                _horizontalMovement.StopMovement();
                break;

            case CharacterState.Walking:
                _horizontalMovement.EnableMovement(true);
                _horizontalMovement.Move(_inputData.HorizontalInput, false);
                break;

            case CharacterState.Running:
                _horizontalMovement.EnableMovement(true);
                _horizontalMovement.Move(_inputData.HorizontalInput, true);
                break;

            case CharacterState.Jump:
                _jumpHandler.PerformJump();
                _animationHandler.AnimateJump();
                break;

            default:
                _horizontalMovement.EnableMovement(false);
                Debug.LogWarning($"Необработанное состояние {_currentState} в CharacterController. Горизонтальное движение отключено.");
                break;
        }
    }
}