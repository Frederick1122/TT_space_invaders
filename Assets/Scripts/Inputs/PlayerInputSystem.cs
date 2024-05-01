using UnityEngine;

namespace Inputs
{
    public class PlayerInputSystem : MonoBehaviour, IInputSystem
    {
        public float HorizontalInput => _horizontalInput;
        private float _horizontalInput;

        protected IInputHandler _inputHandler;

        private void Awake()
        {
#if UNITY_ANDROID 
            _inputHandler = gameObject.AddComponent(typeof(MobileInputHandler)) as IInputHandler;
#else
            _inputHandler = gameObject.AddComponent(typeof(KeyboardInputHandler)) as IInputHandler;
#endif
            
            _inputHandler.OnHorizontalAxisChange += OnHorizontalAxisChange;
        }
        
        protected virtual void OnDestroy()
        {
            if (_inputHandler == null)
                return;

            _inputHandler.OnHorizontalAxisChange += OnHorizontalAxisChange;
        }
        
        protected void OnHorizontalAxisChange(float value) =>
            _horizontalInput = value;
    }
}