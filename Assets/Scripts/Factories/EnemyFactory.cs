using Configs;
using Rx;
using UniRx;
using UnityEngine;

namespace Factories
{
    public class EnemyFactory : Factory//<Enemy, EnemyConfig>
    {
        protected override string _secondPrefabPath => "Enemy";
        
        private CompositeDisposable disposables;

        public override void Init()
        {
            base.Init();
            disposables = new CompositeDisposable();
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_LEVEL)
                .Subscribe(msg => { 
                     SetupNewLevel((LevelConfig)msg.data); 
                }).AddTo (disposables);
        }
        
        void OnDestroy () { 
            if (disposables != null) {
                disposables.Dispose ();
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
                    var position = levelConfig.startPosition + 
                                   new Vector2(-levelConfig.offset.x * (idx % levelConfig.columnCount),
                                       levelConfig.offset.y * (idx / levelConfig.columnCount));
                    ConstructNewObject(sequence.enemy, position);
                    idx++;
                }
            }
        }
    }
}