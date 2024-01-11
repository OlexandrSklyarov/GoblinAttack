
namespace Game.Runtime.Core.FSM.Player.States
{
    public class UnitDamageState : BaseUnitState
    {
        public UnitDamageState(IUnitAgent agent, IUnitSwitchContext context) : base(agent, context)
        {
        }

        public override void OnEnter()
        {
            _agent.Engine.Stop();
            _agent.View.OnDamageCompletedEvent += OnDamageCompleted;
            _agent.View.PlayDamage();
        }

        public override void OnExit()
        {
            _agent.View.OnDamageCompletedEvent -= OnDamageCompleted;
        }

        private void OnDamageCompleted()
        {
            _context.SwitchState<UnitIdleState>();
        }
    }
}