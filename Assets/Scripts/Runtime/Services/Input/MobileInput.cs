using UnityEngine;

namespace Game.Runtime.Services
{
    public class MobileInput : MonoBehaviour, IInputService
    {
        Vector2 IInputService.Movement => _inputAction.Player.Move.ReadValue<Vector2>();
        bool IInputService.IsAttack => _inputAction.Player.Fire.WasPressedThisFrame();
        bool IInputService.IsSpecialAttack => _inputAction.Player.Fire2.WasPressedThisFrame();

        private PlayerInputActions _inputAction;

        private void Awake() 
        {
            _inputAction = new PlayerInputActions();
            _inputAction.Enable();    
        }

        public void Hide() => gameObject.SetActive(false);
    }
}