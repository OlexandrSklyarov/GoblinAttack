
namespace Game.Runtime.Core.Services.ObjectPool
{
    public interface IPoolableItem<T> where T : UnityEngine.MonoBehaviour
    {
        void SetPool(IPool<T> pool);
    }
}