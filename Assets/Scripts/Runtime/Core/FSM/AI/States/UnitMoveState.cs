using Game.Runtime.Core.Enemies;
using UnityEngine;

namespace Game.Runtime.Core.FSM.Player.States
{
    public class UnitMoveState : BaseUnitState
    {
        private float _setDestinationTime;

        public UnitMoveState(IUnitAgent agent, IUnitSwitchContext context) : base(agent, context)
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
            if (!_agent.MyTarget.IsAlive)
            {
                _context.SwitchState<UnitIdleState>();
                return;
            }
            
            if (_agent.IsTargetNear())
            {
                _context.SwitchState<UnitAttackState>();
                return;
            }            

            if (!_agent.MyTarget.IsAlive)
            {
                _context.SwitchState<UnitIdleState>();
                return;
            }

            if (Time.time > _setDestinationTime)
            {
                _setDestinationTime = Time.time + 0.1f;
                _agent.Engine.MoveTo(_agent.MyTarget.Position);
            }

            _agent.View.RotateFromDirection(_agent.Engine.Direction, _agent.Config.Rotation);
            _agent.View.SetSpeed(_agent.Engine.Speed);
        }       

        private void OnDieState(EnemyUnit unit)
        {
            _context.SwitchState<UnitDieState>();
        }

        private void OnDamageState()
        {
            _context.SwitchState<UnitDamageState>();
        }        
    }
}