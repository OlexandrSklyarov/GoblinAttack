using UnityEngine;

namespace Game.Runtime.Services
{
    public interface IInputService
    {
        public Vector2 Movement {get;}
        public bool IsSimpleAttack {get;}
        public bool IsSuperAttack {get;}
    }
}
