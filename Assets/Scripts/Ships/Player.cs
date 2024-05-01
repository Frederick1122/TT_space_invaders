using Configs;
using Inputs;
using Rx;
using UniRx;
using UnityEngine;

namespace Ships
{
    public class Player : Ship
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private PlayerInputSystem _inputSystem;
        
        [SerializeField] private float _speed;
        [SerializeField] private BulletConfig _bulletConfig;

        protected override Vector2 _direction => Vector2.up;

        protected override void OnValidate()
        {
            if (_inputSystem == null)
                _inputSystem = GetComponent<PlayerInputSystem>();

            if (_rb == null)
                _rb = GetComponent<Rigidbody2D>();
            
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
            
            base.Die();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var horizontalInput = _inputSystem.HorizontalInput;

            _rb.velocity = new Vector2(horizontalInput * _speed, 0f);
        }
    }
}