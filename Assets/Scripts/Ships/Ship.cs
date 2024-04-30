using Factories;
using UnityEngine;

namespace Ships
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Ship : PoolObject
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        private void OnValidate()
        {
            if(_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}