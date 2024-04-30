using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : ShipConfig
    {
        public int score = 1;
    }
}