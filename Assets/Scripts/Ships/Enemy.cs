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
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_DESTROY_ENEMY, 
                    this
                ));;
        }
    }
}