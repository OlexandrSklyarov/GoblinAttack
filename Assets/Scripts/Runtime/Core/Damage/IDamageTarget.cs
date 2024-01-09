using UnityEngine;

namespace Game.Runtime.Core.Damage
{
    public interface IDamageTarget
    {
        bool IsAlive {get;}
        Vector3 Position {get;}
        void ApplyDamage(float damage);
    }
}