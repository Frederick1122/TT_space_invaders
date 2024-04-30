using Configs;
using Rx;
using Ships;
using UniRx;

namespace Factories
{
    public class BulletFactory : Factory//<Bullet, BulletConfig>
    {
        protected override string _secondPrefabPath => "Bullet";
        private CompositeDisposable _disposables = new();

        public override void Init()
        {
            base.Init();
            
            MessageBroker.Default
                .Receive<MessageBase>() 
                .Where(msg => msg.id == ServiceShareData.MSG_SPAWN_BULLET)
                .Subscribe(msg =>
                    GenerateBullet((BulletSpawnData)msg.data))
                .AddTo(_disposables);
        }
        
        private void OnDestroy () { 
            if (_disposables != null) {
                _disposables.Dispose ();
            }
        }

        private void GenerateBullet(BulletSpawnData data)
        {
            var newBullet = ConstructNewObject(data.config, data.position.position) as Bullet;
            newBullet.SetDirection(data.direction);
        }
    }
}