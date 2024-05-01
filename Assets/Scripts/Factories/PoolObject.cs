using Configs;
using UnityEngine;

namespace Factories
{
    public abstract class PoolObject : MonoBehaviour
    {
        public abstract void Construct(GameObjectConfig config = null);
    }
}