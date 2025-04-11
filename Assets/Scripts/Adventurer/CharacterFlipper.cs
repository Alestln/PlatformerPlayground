using System;
using UnityEngine;

namespace Assets.Scripts.Adventurer
{
    public class CharacterFlipper : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        private float _previousDirection = 1f;

        public void UpdateDirection(float horizontalInput)
        {
            if (horizontalInput != 0f && horizontalInput != _previousDirection)
            {
                Vector3 scale = _inputHandler.transform.localScale;
                scale.x = horizontalInput;
                _inputHandler.transform.localScale = scale;

                _previousDirection = horizontalInput;
            }
        }
    }
}
