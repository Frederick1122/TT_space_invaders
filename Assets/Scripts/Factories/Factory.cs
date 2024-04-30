using System.Collections.Generic;
using Configs;
using MonoInstallers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Factories
{
    public abstract class Factory : MonoBehaviour, IZenjectInitialization
    {
        private const string FIRST_PREFAB_PATH = "Prefabs/";
        
        protected abstract string _secondPrefabPath { get; }
        protected virtual int _initialPoolSize => 1;

        private ObjectPool _pool;
        private List<PoolObject> _activeObjects = new(); 

        public virtual void Init() 
        {
            var prefab = Resources.Load<PoolObject>(FIRST_PREFAB_PATH + _secondPrefabPath);
            Assert.IsFalse(prefab == null, $"prefab not found. check path: {FIRST_PREFAB_PATH + _secondPrefabPath}");

            _pool = new ObjectPool(prefab, _initialPoolSize);
        }

        public void ConstructNewObject(GameObjectConfig config, Vector2 position)
        {
            var newObject = _pool.Take();
            newObject.Construct(config);
            newObject.transform.position = position;
            newObject.transform.SetParent(transform);
            _activeObjects.Add(newObject);
        }

        public void DeconstructObject(PoolObject poolObject)
        {
            if (_activeObjects.Contains(poolObject))
                _activeObjects.Remove(poolObject);
            
            _pool.Return(poolObject);
        }

        public void DeconstructAllObjects()
        {
            for (var i = _activeObjects.Count - 1; i > -1; i--)
            {
                DeconstructObject(_activeObjects[i]);
            }
        }
    }
}