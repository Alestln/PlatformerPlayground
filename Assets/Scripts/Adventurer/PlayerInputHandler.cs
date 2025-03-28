using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public bool JumpPressed { get; private set; }
    private bool _jumpRequest;

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpRequest = true;
        }
    }

    public void ResetJump()
    {
        JumpPressed = false;
    }

    public bool GetJumpRequest()
    {
        bool jump = _jumpRequest;
        _jumpRequest = false;
        return jump;
    }

}
