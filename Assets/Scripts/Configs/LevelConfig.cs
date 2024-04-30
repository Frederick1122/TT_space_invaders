using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : BaseConfig
    {
        public Vector2 startPosition;
        public Vector2 offset;
        public int columnCount = 5;

        public List<LevelSequence> sequences = new();
    }

    [Serializable]
    public class LevelSequence
    {
        public int count;
        public EnemyConfig enemy;
    }
}