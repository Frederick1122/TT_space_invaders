using System;
using UnityEngine;

namespace Inputs
{
    public class MobileInputHandler : MonoBehaviour, IInputHandler
    {
        public event Action<float> OnHorizontalAxisChange = delegate {  };
        public bool IsActive { get; set; }
        
        private float _horA = 0;

        private void Update()
        {
            if (!IsActive)
                return;

            var horA = 0;

            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2)
                {
                    if (touch.phase == TouchPhase.Began) 
                        horA = -1;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    if (touch.phase == TouchPhase.Began)
                        horA = 1;
                }
            }

            if (_horA != horA)
            {
                _horA = horA;
                OnHorizontalAxisChange?.Invoke(_horA);
            }
        }
    }
}