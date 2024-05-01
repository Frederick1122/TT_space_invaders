using Configs;
using Factories;
using UnityEngine;

namespace Ships
{
    public class Bullet : PoolObject
    {
        private BulletConfig _config;
        private Vector2 _direction = Vector2.zero;
        private bool _isEnemy; 
        
        public override void Construct(GameObjectConfig config)
        {
            _config = (BulletConfig) config;
        }

        public void SetAffiliation(bool isEnemy) => _isEnemy = isEnemy;
        
        public void SetDirection(Vector2 direction) => _direction = direction.normalized;

        public void Update()
        {
            transform.Translate(_direction * _config.speed * Time.deltaTime);
        }
    }
}