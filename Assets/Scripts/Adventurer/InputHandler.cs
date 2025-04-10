using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Предоставляет InputData через публичное свойство
    public InputData CurrentInput { get; private set; }

    void Update()
    {
        // Заполняем структуру CurrentInput
        CurrentInput = new InputData
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal"),
            IsRunHeld = Input.GetKey(KeyCode.LeftShift)
            // Добавь чтение других кнопок сюда
        };
    }

    public void ResetInput()
    {
        // Сбрасываем структуру к значениям по умолчанию
        CurrentInput = default;
    }
}
