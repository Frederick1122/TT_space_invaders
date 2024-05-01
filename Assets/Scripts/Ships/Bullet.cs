using Configs;
using Factories;
using Rx;
using UniRx;
using UnityEngine;

namespace Ships
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Bullet : PoolObject
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private BulletConfig _config;
        private Vector2 _direction = Vector2.zero;
        private int _ignoreLayerIdx;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == _ignoreLayerIdx)
                return;
            
            if (col.TryGetComponent(out Ship ship)) 
                ship.GetDamage(_config.damage);
            
            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_DESTROY_BULLET,
                    this
                ));
        }

        private void OnValidate()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void Update()
        {
            transform.Translate(_direction * _config.speed * Time.deltaTime);
        }

        public override void Construct(GameObjectConfig config)
        {
            _config = (BulletConfig) config;
            _spriteRenderer.sprite = _config.icon;
        }

        public void SetStartData(Vector2 direction, int ignoreLayerIdx)
        {
            _direction = direction.normalized;
            _ignoreLayerIdx = ignoreLayerIdx;
        }
    }
}