
namespace Game.Runtime.Core.FSM.Player.States
{
    public class PlayerDamageState : BasePlayerState
    {
        public PlayerDamageState(IPlayerAgent agent, IPlayerSwitchContext context) : base(agent, context)
        {
        }

        public override void OnEnter()
        {
            _agent.View.OnDamageCompletedEvent += OnDamageCompleted;
            _agent.View.PlayDamage();
        }

        public override void OnExit()
        {
            _agent.View.OnDamageCompletedEvent -= OnDamageCompleted;
        }

        private void OnDamageCompleted()
        {
            if (_agent.IsAlive)
                _context.SwitchState<PlayerIdleState>();
            else
                _context.SwitchState<PlayerDieState>();
        }
    }
}