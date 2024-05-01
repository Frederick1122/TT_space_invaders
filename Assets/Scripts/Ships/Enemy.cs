using System;
using Configs;
using Rx;
using UniRx;
using UnityEngine;

namespace Ships
{
    public class Enemy : Ship
    {
        protected override Vector2 _direction => Vector2.down;

        private int _idx;
        private int _columnCount;
        private Vector2 _offset;
        private EnemyConfig _config;
        
        private IDisposable _moveUpdate;
        private CompositeDisposable _disposables = new();

        protected override void OnDestroy () { 
            _disposables.Dispose();
        }
        
        public void SetLevelData(int idx, int columnCount, Vector2 offset)
        {
            _idx = idx;
            _columnCount = columnCount;
            _offset = offset;
            
            _moveUpdate?.Dispose();

            _moveUpdate = Observable.Timer (TimeSpan.FromSeconds (_cooldown))
                .Repeat () 
                .Subscribe (_ => Move()).AddTo(_disposables);

            Observable.Return(Unit.Default)
                .Delay(TimeSpan.FromSeconds(_cooldown / 2))
                .Subscribe(_ => SetBullet(_config.bulletConfig))
                .AddTo(_disposables);
        }
        
        public override void Construct(GameObjectConfig config)
        {
            _config = (EnemyConfig) config;
            _spriteRenderer.sprite = _config.icon;

            base.Construct(config);
        }

        protected override void Die()
        {
            MessageBroker.Default
                .Publish(MessageBase.Create(
                    this,
                    ServiceShareData.MSG_DESTROY_ENEMY,
                    this
                ));
            ;
        }

        private void Move()
        {
            if (_idx == -1) 
                _idx -= _columnCount;

            if (_idx % _columnCount == 0)
                Move(Vector2.down);
            else
                Move(_idx / _columnCount % 2 == 0 ? Vector2.right : Vector2.left);

            _idx--;
        }

        private void Move(Vector2 direction)
        {
            transform.position += (Vector3)(direction * _offset);
        }
    }
}