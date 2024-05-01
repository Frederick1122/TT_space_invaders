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
        [SerializeField] private Transform _bulletSpawnPosition;
        
        protected ItemConfig _item;
        protected float _cooldowm = 1f;
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
        }

        protected void Shoot()
        {
            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_SPAWN_BULLET, 
                    _bulletSpawnData
                ));
        }
        
        protected void SetBullet(BulletConfig newConfig)
        {
            _shootUpdate?.Dispose();
            
            if (newConfig == null)
                return;
            
            _bulletSpawnData.config = newConfig;
            
            _shootUpdate = Observable.Timer (TimeSpan.FromSeconds (_cooldowm))
                .Repeat () 
                .Subscribe (_ => Shoot()
                ); 
        }
        
        protected abstract void Die();
    }
}