using UnityEngine;
using Zenject;

namespace MonoInstallers
{
    public static class Tools
    {
        public static T GenerateNewInstance<T>(DiContainer container) where T : MonoBehaviour
        {
            var instance = container.InstantiateComponentOnNewGameObject<T>();
            container.Bind<T>().FromInstance(instance).AsSingle().NonLazy();
            
            if (instance is IZenjectInitialization inctanceWithInitialization)
                inctanceWithInitialization.Init();

            return instance;
        }
    }
    
    public interface IZenjectInitialization
    {
        void Init();
    }
}