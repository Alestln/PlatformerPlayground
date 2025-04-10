using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // --- ����������� (����������� � ����������) ---
    [Header("Dependencies")]
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private HorizontalMovement _horizontalMovement;
    [SerializeField] private AnimationHandler _animationHandler; // ������ ������
    // ������ ���� ������ �������� �, ��������, ������ ����� ��������

    private CharacterState _currentState = CharacterState.Idle;

    private void Start()
    {
        UpdateStateAndCommand(_currentState, default);
    }

    private void Update()
    {
        // 1. �������� InputData
        InputData input = _inputHandler.CurrentInput;

        // 2. ���������� ������� ��������� �� ������ InputData
        CharacterState newState = DetermineState(input);

        // 3. ��������� ��������� � ������ �������, ��������� InputData
        UpdateStateAndCommand(newState, input.HorizontalInput);
    }

    private CharacterState DetermineState(InputData input)
    {
        if (!Mathf.Approximately(input.HorizontalInput, 0f))
        {
            return input.IsRunHeld ? CharacterState.Running : CharacterState.Walking;
        }
        else
        {
            return CharacterState.Idle;
        }
    }

    private void UpdateStateAndCommand(CharacterState newState, float currentHorizontalInput)
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
                _horizontalMovement.Move(currentHorizontalInput, false);
                break;

            case CharacterState.Running:
                _horizontalMovement.EnableMovement(true);
                _horizontalMovement.Move(currentHorizontalInput, true);
                break;

            default:
                _horizontalMovement.EnableMovement(false);
                Debug.LogWarning($"�������������� ��������� {_currentState} � CharacterController. �������������� �������� ���������.");
                break;
        }
    }
}