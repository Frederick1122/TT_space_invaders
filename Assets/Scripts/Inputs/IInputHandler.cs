using System;

namespace Inputs
{
    public interface IInputHandler
    {
        public event Action<float> OnHorizontalAxisChange;
        public event Action<float> OnVerticalAxisChange;
        
        public bool IsActive { get; set; }
    }
}