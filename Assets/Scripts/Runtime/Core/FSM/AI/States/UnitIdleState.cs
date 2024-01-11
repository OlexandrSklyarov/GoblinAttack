using Game.Runtime.Core.Enemies;

namespace Game.Runtime.Core.FSM.Player.States
{
    public class UnitIdleState : BaseUnitState
    {
        public UnitIdleState(IUnitAgent agent, IUnitSwitchContext context) : base(agent, context)
        {
        }

        public override void OnEnter()
        {
            _agent.Engine.Stop();
            _agent.View.SetSpeed(0f);
            
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
                return;
            }

            if (_agent.IsTargetNear())
            {
                _context.SwitchState<UnitAttackState>();
                return;
            } 
            else
            {
                _context.SwitchState<UnitMoveState>();
                return;
            }
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