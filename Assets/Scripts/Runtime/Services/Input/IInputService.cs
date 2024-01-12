using UnityEngine;

namespace Game.Runtime.Services.Input
{
    public interface IInputService
    {
        public Vector2 Movement {get;}
        public bool IsAttack {get;}
        public bool IsSpecialAttack {get;}

        void Enable();
        void Disable();
    }
}
