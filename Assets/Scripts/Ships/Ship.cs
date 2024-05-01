using System;
using Configs;
using Factories;
using Rx;
using UniRx;
using UnityEngine;

namespace Ships
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Ship : PoolObject
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Transform _bulletSpawnPosition;

        private int _hp = 1;
        protected virtual Vector2 _direction => Vector2.zero;
        
        private BulletSpawnData _bulletSpawnData = new();
        private IDisposable _shootUpdate;
        
        protected virtual void OnValidate()
        {
            if(_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        protected virtual void OnDestroy () {
            _shootUpdate?.Dispose();
        }
        
        public override void Construct(GameObjectConfig config = null)
        {
            _bulletSpawnData.position = _bulletSpawnPosition;
            _bulletSpawnData.direction = _direction;
            _bulletSpawnData.layerIndex = gameObject.layer;

            _hp = 1;
            
            if (config == null)
                return;
            
            var shipConfig = (ShipConfig) config;
            _hp = shipConfig.hp;
        }

        public void GetDamage(int damage)
        {
            _hp -= damage;
            
            if (_hp <= 0)
                Die();
        }

        public void SetBullet(BulletConfig newConfig)
        {
            _shootUpdate?.Dispose();
            
            if (newConfig == null)
                return;
            
            _bulletSpawnData.config = newConfig;
            
            _shootUpdate = Observable.Timer (TimeSpan.FromSeconds (newConfig.cooldown))
                .Repeat () 
                .Subscribe (_ => Shoot()); 
        }

        protected virtual void Shoot()
        {
            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_SPAWN_BULLET, 
                    _bulletSpawnData
                ));
        }

        protected virtual void Die()
        {
            _shootUpdate?.Dispose();   
        }
    }
}