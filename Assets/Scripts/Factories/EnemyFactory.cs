using Configs;
using Rx;
using Ships;
using UniRx;
using UnityEditorInternal;
using UnityEngine;

namespace Factories
{
    public class EnemyFactory : Factory//<Enemy, EnemyConfig>
    {
        protected override string _secondPrefabPath => "Enemy";
        
        private CompositeDisposable _disposables = new();

        public override void Init()
        {
            base.Init();
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_SETUP_NEW_LEVEL)
                .Subscribe(msg => 
                     SetupNewLevel((LevelConfig)msg.data))
                .AddTo(_disposables);
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_DESTROY_ENEMY)
                .Subscribe(msg =>  
                    DeconstructObject((Enemy)msg.data))
                .AddTo(_disposables);

            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_RESET_LEVEL)
                .Subscribe(_ => DeconstructAllObjects())
                .AddTo(_disposables);
        }
        
       private void OnDestroy () { 
            if (_disposables != null) {
                _disposables.Dispose ();
            }
        }

        private void SetupNewLevel(LevelConfig levelConfig)
        {
            DeconstructAllObjects();

            var idx = 0;
            foreach (var sequence in levelConfig.sequences)
            {
                for (var i = 0; i < sequence.count; i++)
                {
                    var currentOffset = Vector2.zero;
                    currentOffset.y = levelConfig.offset.y * (idx / levelConfig.columnCount);
                    if (idx / levelConfig.columnCount % 2 == 0)
                        currentOffset.x = -levelConfig.offset.x * (idx % levelConfig.columnCount);
                    else
                        currentOffset.x = -levelConfig.offset.x *
                                          (levelConfig.columnCount - idx % levelConfig.columnCount - 1);

                    var position = levelConfig.startPosition + currentOffset; 
                                   
                    var newEnemy = ConstructNewObject(sequence.enemy, position) as Enemy;
                    newEnemy.SetLevelData(idx, levelConfig.columnCount, levelConfig.offset);
                    idx++;
                }
            }
        }
    }
}