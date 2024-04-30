using Zenject;

namespace MonoInstallers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var level = Tools.GenerateNewInstance<Level.Level>(Container);
            level.SetupLevel();
        }
    }
}