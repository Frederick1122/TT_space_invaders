using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Factories
{
    public class ObjectPool
    {
        private Queue<PoolObject> _pool;
        private PoolObject _prefab;
        
        public ObjectPool(PoolObject prefab, int initialSize)
        {
            _prefab = prefab;
            _pool = new Queue<PoolObject>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                var instance = Object.Instantiate(prefab);
                instance.gameObject.SetActive(false);
                _pool.Enqueue(instance);
            }
        }

        public PoolObject Take()
        {
            PoolObject instance;
            if (_pool.Count == 0)
                instance = Object.Instantiate(_prefab);
            else
                instance = _pool.Dequeue();

            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Return(PoolObject instance)
        {
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }
    }
}