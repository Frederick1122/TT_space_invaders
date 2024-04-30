using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/ItemConfig", fileName = "ItemConfig")]
    public class ItemConfig : GameObjectConfig
    {
        public BulletConfig bullet;
    }
}