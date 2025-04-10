using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;

    private static readonly int StateIdle = Animator.StringToHash(AnimationParameters.IsIdle);
    private static readonly int StateWalking = Animator.StringToHash(AnimationParameters.IsWalking);
    private static readonly int StateRunning = Animator.StringToHash(AnimationParameters.IsRunning);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateStateAnimation(CharacterState state)
    {
        _animator.SetBool(StateIdle, state == CharacterState.Idle);
        _animator.SetBool(StateWalking, state == CharacterState.Walking);
        _animator.SetBool(StateRunning, state == CharacterState.Running);
    }

    public void SetVisualDirection(float horizontalDirection)
    {
        if (Mathf.Approximately(horizontalDirection, 0f))
        {
            return;
        }

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(horizontalDirection) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}