using Configs;
using Factories;
using Rx;
using UniRx;
using UnityEngine;

namespace Ships
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Item : PoolObject
    {
        private const float SPEED = 1f;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private ItemConfig _config;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player)) 
                player.SetBullet(_config.bullet);
            
            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_DESTROY_ITEM,
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
            transform.Translate(Vector2.down * SPEED * Time.deltaTime);
        }
        
        public override void Construct(GameObjectConfig config)
        {
            _config = (ItemConfig) config;
            _spriteRenderer.sprite = _config.icon;
        }
    }
}