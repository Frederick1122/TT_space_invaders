using System.Collections.Generic;
using System.Linq;
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

        private int _enemyCount;
        private int _currentLevelIdx;
        private CompositeDisposable _disposables = new();

        public void Init()
        {
            _levelConfigs.AddRange(Resources.LoadAll<LevelConfig>(LEVEL_CONFIGS_PATH));
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_DESTROY_ENEMY)
                .Subscribe(_ => DestroyEnemy()).AddTo(_disposables);
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_RESET_LEVEL)
                .Subscribe(_ => ResetLevel()).AddTo(_disposables);
        }
        
        private void OnDestroy () { 
            if (_disposables != null) {
                _disposables.Dispose ();
            }
        }

        public void SetupLevel(int idx = -1)
        {
            idx = idx == -1 ? 0 : idx;

            _currentLevelIdx = idx;
            _enemyCount = _levelConfigs[idx].sequences.Sum(s => s.count);
            
            MessageBroker.Default
                .Publish (MessageBase.Create (
                    this, 
                    ServiceShareData.MSG_SETUP_NEW_LEVEL, 
                    _levelConfigs[idx]
                ));
        }

        private void DestroyEnemy()
        {
            _enemyCount--;

            if (_enemyCount == 0)
            {
                SetupLevel();
            }
        }

        private void ResetLevel()
        {
            SetupLevel(_currentLevelIdx);
        }
    }
}