using Game.Runtime.Core.Enemies;

namespace Game.Runtime.Core.FSM.Player.States
{
    public abstract class BaseUnitAttackState : BaseUnitState
    {
        public BaseUnitAttackState(IUnitAgent agent, IUnitSwitchContext context) : base(agent, context)
        {            
        }

        public override void OnEnter()
        {         
            _agent.Engine.Stop();   
            _agent.View.OnAttackExecuteEvent += AttackExecute;
            _agent.View.OnAttackCompletedEvent += AttackCompleted;
            _agent.OnDieEvent += OnDieState;
            _agent.OnDamageEvent += OnDamageState;  
        }

        public override void OnExit()
        {
            _agent.View.OnAttackExecuteEvent -= AttackExecute;
            _agent.View.OnAttackCompletedEvent -= AttackCompleted;
            _agent.OnDieEvent -= OnDieState;
            _agent.OnDamageEvent -= OnDamageState;
        }        

        public override void OnUpdate()
        {   
            RotateToCurrentTarget();

            if (!_agent.MyTarget.IsAlive)
            {
                _context.SwitchState<UnitIdleState>();
                return;
            }
        }

        private void AttackCompleted()
        {
            _context.SwitchState<UnitIdleState>();
        }   

        private void AttackExecute()
        {
            if (_agent.MyTarget != null && _agent.MyTarget.IsAlive && _agent.IsTargetNear())
            {
                _agent.MyTarget.ApplyDamage(_agent.Config.Attack.Damage);
            }
        }

        private void RotateToCurrentTarget()
        {
            if (_agent.MyTarget == null) return;
            
            var dir = (_agent.MyTarget.Position - _agent.MyTransform.position).normalized;
            _agent.View.RotateFromDirection(dir, _agent.Config.Rotation);
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