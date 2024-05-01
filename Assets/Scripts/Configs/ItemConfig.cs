using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/ItemConfig", fileName = "ItemConfig")]
    public class ItemConfig : GameObjectConfig
    {
        public BulletConfig bullet;
    }

    public class ItemSpawnData
    {
        public Transform position;
        public ItemConfig config;
    }
}