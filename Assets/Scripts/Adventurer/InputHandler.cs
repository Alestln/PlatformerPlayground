using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // ������������� InputData ����� ��������� ��������
    public InputData CurrentInput { get; private set; }

    void Update()
    {
        // ��������� ��������� CurrentInput
        CurrentInput = new InputData
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal"),
            IsRunHeld = Input.GetKey(KeyCode.LeftShift)
            // ������ ������ ������ ������ ����
        };
    }

    public void ResetInput()
    {
        // ���������� ��������� � ��������� �� ���������
        CurrentInput = default;
    }
}
