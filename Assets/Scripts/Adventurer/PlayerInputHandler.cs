using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float Horizontal { get; private set; }

    private bool _jumpRequest;
    private bool _dashRequest;

    public delegate void DashRequestHandler();
    public event DashRequestHandler OnDashRequested;

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpRequest = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dashRequest = true;
            OnDashRequested?.Invoke();
        }
    }

    public void ResetJump()
    {
        _jumpRequest = false;
    }

    public bool GetJumpRequest()
    {
        bool jump = _jumpRequest;
        _jumpRequest = false;
        return jump;
    }
    
    public bool GetDashRequest()
    {
        bool dash = _dashRequest;
        _dashRequest = false;
        return dash;
    }
}
