using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private PlayerInputHandler _input;

    private void Awake()
    {
        _input = GetComponentInParent<PlayerInputHandler>();
    }

    private void Update()
    {
        HandleFlip();
    }

    private void HandleFlip()
    {
        if (_input.Horizontal != 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Sign(_input.Horizontal);
            _input.gameObject.transform.localScale = localScale; // Нужно быть уверенным, что PlayerInputHandler на корне объекта.
        }
    }
}
