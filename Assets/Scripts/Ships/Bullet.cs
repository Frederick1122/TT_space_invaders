using System;
using Configs;
using Factories;
using UnityEngine;

namespace Ships
{
    public class Bullet : PoolObject
    {
        private BulletConfig _config;
        private Vector2 _direction = Vector2.zero;
        
        public override void Construct(GameObjectConfig config)
        {
            _config = (BulletConfig) config;
        }

        public void SetDirection(Vector2 direction) => _direction = direction.normalized;

        public void Update()
        {
            transform.Translate(_direction * _config.speed * Time.deltaTime);
        }
    }
}