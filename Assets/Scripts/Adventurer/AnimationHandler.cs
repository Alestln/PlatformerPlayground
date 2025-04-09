using UnityEngine;

namespace Assets.Scripts.Adventurer
{
    [RequireComponent(typeof(Animator))]
    public class AnimationHandler : MonoBehaviour
    {
        private Animator _animator;
        private StateController _controller;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _controller = GetComponentInParent<StateController>();
        }

        private void Update()
        {
            switch (_controller.CurrentState)
            {
                case State.Idle:
                    break;
                case State.Walking:
                    Flip();
                    break;
                case State.Running:
                    Flip();
                    break;
            }
        }

        private void Flip()
        {
            if (_controller.Direction.x != 0)
            {
                Vector3 scale = _controller.transform.localScale;
                scale.x = _controller.Direction.x * Mathf.Abs(scale.x);
                _controller.transform.localScale = scale;
            }
        }
    }
}
