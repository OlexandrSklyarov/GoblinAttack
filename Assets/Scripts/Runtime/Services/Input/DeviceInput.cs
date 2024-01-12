using UnityEngine;

namespace Game.Runtime.Services.Input
{
    public class DeviceInput : IInputService
    {
        Vector2 IInputService.Movement => _inputAction.Mobile.Move.ReadValue<Vector2>();
        bool IInputService.IsAttack => _inputAction.Mobile.Fire.WasPressedThisFrame();
        bool IInputService.IsSpecialAttack => _inputAction.Mobile.Fire2.WasPressedThisFrame();

        private readonly PlayerInputActions _inputAction;


        public DeviceInput()
        {
            _inputAction = new PlayerInputActions();
            _inputAction.Enable();
            _inputAction.PC.Disable();
        }

        void IInputService.Enable()
        {
            _inputAction.Enable();
        }

        void IInputService.Disable()
        {
            _inputAction.Disable();
        }
    }
}