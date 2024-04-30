using Configs;

namespace Ships
{
    public class Enemy : Ship
    {
        public override void Construct(GameObjectConfig config)
        {
            var castConfig = (EnemyConfig) config;
            _spriteRenderer.sprite = castConfig.icon;
        }
    }
}