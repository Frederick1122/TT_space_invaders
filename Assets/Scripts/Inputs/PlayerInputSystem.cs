using UnityEngine;

namespace Inputs
{
    public class PlayerInputSystem : MonoBehaviour, IInputSystem
    {
        public float HorizontalInput => _horizontalInput;
        public float VerticalInput => _verticalInput;
        
        private float _horizontalInput;
        private float _verticalInput;

        private IInputHandler _inputHandler;

        private void Awake()
        {
// #if UNITY_ANDROID 
//             _inputHandler = gameObject.AddComponent(typeof(MobileInputHandler)) as IInputHandler;
// #else
            _inputHandler = gameObject.AddComponent(typeof(KeyboardInputHandler)) as IInputHandler;
// #endif
            
            _inputHandler.OnHorizontalAxisChange += OnHorizontalAxisChange;
            _inputHandler.OnVerticalAxisChange += OnVerticalAxisChange;
        }
        
        protected virtual void OnDestroy()
        {
            if (_inputHandler == null)
                return;

            _inputHandler.OnHorizontalAxisChange -= OnHorizontalAxisChange;
            _inputHandler.OnVerticalAxisChange -= OnVerticalAxisChange;
        }
        
        private void OnHorizontalAxisChange(float value) =>
            _horizontalInput = value;

        private void OnVerticalAxisChange(float value) =>
            _verticalInput = value;
    }
}