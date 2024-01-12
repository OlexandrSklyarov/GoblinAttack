using UnityEngine;
using UnityEngine.Pool;

namespace Game.Runtime.Core.Services.ObjectPool
{
    public class UniversalPoolGO<T> : IPool<T> where T : MonoBehaviour, IPoolableItem<T>
    {
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly IObjectPool<T>_innerPool;

        public UniversalPoolGO(T prefab, string poolName, int startCount = 10, int maxCount = 10000)
        {            
            _prefab = prefab;
            _container = new GameObject($"[{poolName}]").transform;
            
            _innerPool = new ObjectPool<T>
            (
                OnCreateItem, OnTakeItem, OnReturnItem, OnDestroyItem, true, startCount, maxCount
            );            
        }

        private void OnDestroyItem(T decal)
        {
            UnityEngine.Object.Destroy(decal.gameObject);
        }

        private void OnReturnItem(T decal)
        {
            decal.gameObject.SetActive(false);
        }

        private void OnTakeItem(T decal)
        {
            decal.gameObject.SetActive(true);
        }

        private T OnCreateItem()
        {
            var item = UnityEngine.Object.Instantiate(_prefab, _container);
            item.SetPool(this);
            return item;            
        }

        public T Get()
        {
            return _innerPool.Get();
        }

        public void Clear()
        {
            _innerPool.Clear();
        }

        void IPool<T>.Reclaim(IPoolableItem<T> item) => _innerPool.Release(item as T);
    }
}