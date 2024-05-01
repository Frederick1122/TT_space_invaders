using System;
using UnityEngine;

namespace Inputs
{
    public class KeyboardInputHandler : MonoBehaviour, IInputHandler
    {
        private const string HORIZONTAl_AXIS = "Horizontal";

        public event Action<float> OnHorizontalAxisChange = delegate {  };
        public bool IsActive { get; set; } = true;
        
        private float _horA = 0;

        private void Update()
        {
            if (!IsActive)
                return;

            var horA = Input.GetAxis(HORIZONTAl_AXIS);
            
            if (_horA != horA)
            {
                _horA = horA;
                OnHorizontalAxisChange?.Invoke(_horA);
            }
        }
    }
}