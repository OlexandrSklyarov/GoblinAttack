using UnityEngine;

namespace Game.Runtime.Core.Services.ObjectPool
{
    public interface IPool<T>  where T : MonoBehaviour
    {
        void Reclaim(IPoolableItem<T> item);
    }
}