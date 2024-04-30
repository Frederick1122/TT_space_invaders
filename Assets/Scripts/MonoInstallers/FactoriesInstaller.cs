using Factories;
using Zenject;

namespace MonoInstallers
{
    public class FactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Tools.GenerateNewInstance<EnemyFactory>(Container);
            Tools.GenerateNewInstance<BulletFactory>(Container);
            Tools.GenerateNewInstance<ItemFactory>(Container);
        }
    }
}