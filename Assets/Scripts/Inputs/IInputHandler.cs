using System;

namespace Inputs
{
    public interface IInputHandler
    {
        public event Action<float> OnHorizontalAxisChange;
        
        public bool IsActive { get; set; }
    }
}