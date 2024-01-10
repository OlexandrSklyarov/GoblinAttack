using UnityEngine;

namespace Game.Runtime.Core.FSM.Player.States
{
    public class PlayerIdleState : BasePlayerState
    {
        public PlayerIdleState(IPlayerAgent agent, IPlayerSwitchContext context) : base(agent, context)
        {
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

            if (_agent.Input.Movement.sqrMagnitude > Mathf.Epsilon)
            {
                _context.SwitchState<PlayerMoveState>();
                return;
            }

            _agent.View.SetSpeed(_agent.Engine.Speed);
        }

        private void OnDieState()
        {
            _context.SwitchState<PlayerDieState>();
        }

        private void OnDamageState()
        {
            _context.SwitchState<PlayerDamageState>();
        }
    }
}