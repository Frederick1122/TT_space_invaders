using Configs;
using Inputs;
using Rx;
using UniRx;
using UnityEngine;

namespace Ships
{
    public class Player : Ship
    {
        [SerializeField] private PlayerInputSystem _inputSystem;
        [SerializeField] private float _speed;
        [SerializeField] private BulletConfig _bulletConfig;

        protected override Vector2 _direction => Vector2.up;

        protected override void OnValidate()
        {
            if (_inputSystem == null)
                _inputSystem = GetComponent<PlayerInputSystem>();
            
            base.OnValidate();
        }

        public override void Construct(GameObjectConfig config = null)
        {
            base.Construct(config);
            SetBullet(_bulletConfig);   
            Input.simulateMouseWithTouches = true;
        }

        protected override void Die()
        {
            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_RESET_LEVEL
                ));
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var horizontalInput = _inputSystem.HorizontalInput;
            transform.Translate(Vector2.left * horizontalInput * _speed * Time.deltaTime);
        }
    }
}