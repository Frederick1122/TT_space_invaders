using System.Collections.Generic;
using Configs;
using MonoInstallers;
using Rx;
using UniRx;
using UnityEngine;

namespace Level
{
    public class Level : MonoBehaviour, IZenjectInitialization
    {
        private static string LEVEL_CONFIGS_PATH = "Configs/Levels";
        private List<LevelConfig> _levelConfigs = new();

        public void Init()
        {
            _levelConfigs.AddRange(Resources.LoadAll<LevelConfig>(LEVEL_CONFIGS_PATH));
        }

        public void SetupLevel(int idx = -1)
        {
            idx = idx == -1 ? 0 : idx;
            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_LEVEL, 
                    _levelConfigs[idx]
                ));
        }
    }
}