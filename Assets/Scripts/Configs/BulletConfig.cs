using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/BulletConfig", fileName = "BulletConfig")]
    public class BulletConfig : GameObjectConfig
    {
        public int damage;
        public float speed;
    }

    public class BulletSpawnData
    {
        public Transform position;
        public Vector2 direction;
        public BulletConfig config;
    }
}