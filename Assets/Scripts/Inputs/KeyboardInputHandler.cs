using System;
using UnityEngine;

namespace Inputs
{
    public class KeyboardInputHandler : MonoBehaviour, IInputHandler
    {
        private const string HORIZONTAl_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";

        public event Action<float> OnHorizontalAxisChange = delegate {  };
        public event Action<float> OnVerticalAxisChange = delegate {  };
        public bool IsActive { get; set; } = true;
        
        private float _horA = 0;
        private float _verA = 0;

        private void Update()
        {
            if (!IsActive)
                return;

            var horA = Input.GetAxis(HORIZONTAl_AXIS);
            var verA = Input.GetAxis(VERTICAL_AXIS);
            
            if (_horA != horA)
            {
                _horA = horA;
                OnHorizontalAxisChange?.Invoke(_horA);
            }

            if (_verA != verA)
            {
                _verA = verA;
                OnVerticalAxisChange?.Invoke(_verA);
            }
        }
    }
}