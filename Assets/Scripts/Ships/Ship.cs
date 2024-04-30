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
        private CompositeDisposable _disposables = new();

        private void OnValidate()
        {
            if(_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void OnDestroy () { 
            if (_disposables != null) {
                _disposables.Dispose ();
            }
        }
        
        public override void Construct(GameObjectConfig config)
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
            _disposables.Dispose();
            _disposables = new CompositeDisposable();
            
            if (newConfig == null)
                return;
            
            _bulletSpawnData.config = newConfig;
            
            Observable.Timer (System.TimeSpan.FromSeconds (_cooldowm))
                .Repeat () 
                .Subscribe (_ => Shoot()
                ).AddTo (_disposables); 
        }
        
        protected abstract void Die();
    }
}