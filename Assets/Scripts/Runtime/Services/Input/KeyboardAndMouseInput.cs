using UnityEngine;

namespace Game.Runtime.Services.Input
{
    public class KeyboardAndMouseInput : IInputService
    {
        Vector2 IInputService.Movement => _inputAction.PC.Move.ReadValue<Vector2>();
        bool IInputService.IsAttack => _inputAction.PC.Fire.WasPressedThisFrame();
        bool IInputService.IsSpecialAttack => _inputAction.PC.Fire2.WasPressedThisFrame();

        private readonly PlayerInputActions _inputAction;


        public KeyboardAndMouseInput()
        {
            _inputAction = new PlayerInputActions();
            _inputAction.Enable();
            _inputAction.Mobile.Disable();
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