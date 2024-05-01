using System;
using Configs;
using Factories;
using Rx;
using UniRx;
using UnityEngine;

namespace Ships
{
    public class Bullet : PoolObject
    {
        private BulletConfig _config;
        private Vector2 _direction = Vector2.zero;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Ship ship)) 
                ship.GetDamage(_config.damage);
            
            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_DESTROY_BULLET,
                    this
                ));

        }

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