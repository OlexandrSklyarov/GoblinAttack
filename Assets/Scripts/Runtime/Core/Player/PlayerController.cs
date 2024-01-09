using Game.Runtime.Core.Components;
using Game.Runtime.Data.Configs;
using Game.Runtime.Services;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Core.Player
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public Transform TargetPoint { get; private set; }
        [field: SerializeField] public MovingEngine Engine{ get; private set; }
        [field: SerializeField] public CharacterView View{ get; private set; }
        
        [SerializeField] private PlayerConfig _config;
        
        private IInputService _input;
        private Transform _cameraMain;
        private Vector3 _moveDirection;

        [Inject]
        private void Construct(IInputService input)
        {
            _input = input;
        }

        private void Awake() 
        {
            _cameraMain = Camera.main.transform;   
        }      

        public Vector3 GetRelativeCameraDirection()
        {         
            var camForward = _cameraMain.forward;           
            var camRight = _cameraMain.right;           
            camForward.y = camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            var vectorRotateToCameraSpace = _input.Movement.y * camForward + _input.Movement.x * camRight;       

            return vectorRotateToCameraSpace;
        }

        private void FixedUpdate() 
        {
            _moveDirection = GetRelativeCameraDirection();
            Engine.Move(_moveDirection, _config.Moving);            
        }

        private void Update() 
        {
            View.RotateFromDirection(_moveDirection, _config.Rotation);
            View.SetSpeed(Engine.Speed);
        }
    }
}