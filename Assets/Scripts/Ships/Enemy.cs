using System;
using Configs;
using Rx;
using UniRx;
using UnityEngine;

namespace Ships
{
    public class Enemy : Ship
    {
        // delete this
        public bool isDead;

        private void Update()
        {
            if (!isDead)
                return;

            isDead = false;
            Die();
        }

        //
        
        protected override Vector2 _direction => Vector2.down;

        private int _idx;
        private int _columnCount;
        private Vector2 _offset;

        private IDisposable _moveUpdate;

        protected override void OnDestroy () { 
            _moveUpdate?.Dispose();
        }
        
        public void SetLevelData(int idx, int columnCount, Vector2 offset)
        {
            _idx = idx;
            _columnCount = columnCount;
            _offset = offset;
            
            _moveUpdate?.Dispose();

            _moveUpdate = Observable.Timer (TimeSpan.FromSeconds (_cooldowm))
                .Repeat () 
                .Subscribe (_ => Move()
                );
        }
        
        public override void Construct(GameObjectConfig config)
        {
            var castConfig = (EnemyConfig) config;
            _spriteRenderer.sprite = castConfig.icon;
            SetBullet(castConfig.bulletConfig);

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
            if (_idx % _columnCount == 0)
                Move(Vector2.down);
            else
                Move(_idx / _columnCount % 2 == 0 ? Vector2.left : Vector2.right);

            _idx--;
        }

        private void Move(Vector2 direction)
        {
            transform.position += (Vector3)(direction * _offset);
        }
    }
}