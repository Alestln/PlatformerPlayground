using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;

    private bool _isGrounded = true;

    private static readonly int StateIdle = Animator.StringToHash(AnimationParameters.IsIdle);
    private static readonly int StateWalking = Animator.StringToHash(AnimationParameters.IsWalking);
    private static readonly int StateRunning = Animator.StringToHash(AnimationParameters.IsRunning);
    private static readonly int StateJump = Animator.StringToHash(AnimationParameters.JumpTrigger);
    private static readonly int StateDoubleJump = Animator.StringToHash(AnimationParameters.DoubleJumpTrigger);
    private static readonly int IsGroundedParam = Animator.StringToHash(AnimationParameters.IsGrounded);

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

    public void AnimateJump()
    {
        if (_isGrounded)
        {
            _animator.SetTrigger(StateJump);
        }
        else
        {
            _animator.SetTrigger(StateDoubleJump);
        }
    }

    public void SetGrounded(bool isGrounded)
    {
        _isGrounded = isGrounded;
        _animator.SetBool(IsGroundedParam, isGrounded);
    }
}