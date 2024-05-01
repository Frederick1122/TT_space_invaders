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
        [SerializeField] private BulletConfig _startBulletConfig;

        protected override Vector2 _direction => Vector2.up;
        private Vector2 _startPosition;
        
        private CompositeDisposable _disposables = new();

        protected override void OnValidate()
        {
            if (_inputSystem == null)
                _inputSystem = GetComponent<PlayerInputSystem>();

            if (_rb == null)
                _rb = GetComponent<Rigidbody2D>();
            
            base.OnValidate();
        }
        
        private void FixedUpdate()
        {
            Move();
        }

        protected override void OnDestroy()
        {
            _disposables.Dispose();
            base.OnDestroy();
        }

        public override void Construct(GameObjectConfig config = null)
        {
            base.Construct(config);
            _startPosition = transform.position;
            Reset();
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_RESET_LEVEL)
                .Subscribe(_ => Reset())
                .AddTo(_disposables);
        }

        protected override void Die()
        {
            base.Die();

            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_RESET_LEVEL
                ));
        }

        private void Reset()
        {
            transform.position = _startPosition;
            SetBullet(_startBulletConfig);
        }
        
        private void Move()
        {
            var horizontalInput = _inputSystem.HorizontalInput;
            var verticalInput = _inputSystem.VerticalInput;

            _rb.velocity = new Vector2(horizontalInput * _speed, verticalInput * _speed);
        }
    }
}