using Configs;
using Rx;
using UniRx;
using UnityEngine;

namespace Ships
{
    public class Player : Ship
    {
        [SerializeField] private float _speed;
        [SerializeField] private BulletConfig _bulletConfig;

        protected override Vector2 _direction => Vector2.up;

        public override void Construct(GameObjectConfig config)
        {
            base.Construct(config);
            SetBullet(_bulletConfig);   
        }

        protected override void Die()
        {
            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_RESET_LEVEL
                ));
        }
    }
}