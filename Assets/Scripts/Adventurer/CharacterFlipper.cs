using System;
using UnityEngine;

namespace Assets.Scripts.Adventurer
{
    public class CharacterFlipper : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;

        private void Update()
        {
            UpdateDirection(_inputHandler.CurrentInput.HorizontalInput);
        }

        private void UpdateDirection(float horizontalInput)
        {
            if (horizontalInput != 0f)
            {
                Vector3 scale = transform.localScale;
                scale.x = horizontalInput;
                transform.localScale = scale;
            }
        }
    }
}
