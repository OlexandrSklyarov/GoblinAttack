using UnityEngine;

namespace Game.Runtime.Services
{
    public class DeviceInput : IInputService
    {
        Vector2 IInputService.Movement => _inputAction.Player.Move.ReadValue<Vector2>();
        bool IInputService.IsSimpleAttack => _inputAction.Player.Fire.WasPressedThisFrame();
        bool IInputService.IsSuperAttack => _inputAction.Player.Fire2.WasPressedThisFrame();

        private readonly PlayerInputActions _inputAction;


        public DeviceInput()
        {
            _inputAction = new PlayerInputActions();
            _inputAction.Enable();
        }
    }
}