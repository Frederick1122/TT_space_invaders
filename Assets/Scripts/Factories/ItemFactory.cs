using Configs;
using Rx;
using Ships;
using UniRx;

namespace Factories
{
    public class ItemFactory : Factory//<Item, ItemConfig>
    {
        protected override string _secondPrefabPath => "Item";
        
        private CompositeDisposable _disposables = new();

        public override void Init()
        {
            base.Init();
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_SPAWN_ITEM)
                .Subscribe(msg => 
                    GenerateNewItem((ItemSpawnData)msg.data))
                .AddTo(_disposables);
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_DESTROY_ITEM)
                .Subscribe(msg =>  
                    DeconstructObject((Item)msg.data))
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

        private void GenerateNewItem(ItemSpawnData spawnData)
        {
            ConstructNewObject(spawnData.config, spawnData.position.position);
        }
    }
}