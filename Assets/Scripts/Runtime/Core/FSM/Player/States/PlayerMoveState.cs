using UnityEngine;

namespace Game.Runtime.Core.FSM.Player.States
{
    public class PlayerMoveState : BasePlayerState
    {
        private Vector3 _moveDirection;
        private Transform _cameraMain;

        public PlayerMoveState(IPlayerAgent agent, IPlayerSwitchContext context) : base(agent, context)
        {
            _cameraMain = Camera.main.transform;
        }

        public override void OnEnter()
        {
            _agent.OnDieEvent += OnDieState;
            _agent.OnDamageEvent += OnDamageState;
        }

        public override void OnExit()
        {
            _agent.OnDieEvent -= OnDieState;
            _agent.OnDamageEvent -= OnDamageState;
        }

        public override void OnUpdate()
        {
            if (_agent.Input.IsAttack)
            {
                _context.SwitchState<PlayerAttackState>();
                return;
            }

            if (_agent.Input.IsSpecialAttack && _agent.Stats.SpecialAttackCooldown <= 0f)
            {
                _context.SwitchState<PlayerSpecialAttackState>();
                return;
            }

            if (_agent.Engine.Speed <= Mathf.Epsilon)
            {
                _context.SwitchState<PlayerIdleState>();
                return;
            }

            _agent.View.RotateFromDirection(_moveDirection, _agent.Config.Rotation);
            _agent.View.SetSpeed(_agent.Engine.Speed);
        }

        public override void OnFixedUpdate()
        {
            _moveDirection = GetRelativeCameraDirection();
            _agent.Engine.Move(_moveDirection, _agent.Config.Moving); 
        }

        private void OnDieState()
        {
            _context.SwitchState<PlayerDieState>();
        }

        private void OnDamageState()
        {
            _context.SwitchState<PlayerDamageState>();
        }

        private Vector3 GetRelativeCameraDirection()
        {         
            var camForward = _cameraMain.forward;           
            var camRight = _cameraMain.right;           
            camForward.y = camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            var vectorRotateToCameraSpace = _agent.Input.Movement.y * camForward + 
                _agent.Input.Movement.x * camRight;       

            return vectorRotateToCameraSpace;
        }
    }
}