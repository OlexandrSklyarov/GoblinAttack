
namespace Game.Runtime.Core.FSM.Player.States
{
    public class PlayerDieState : BasePlayerState
    {
        public PlayerDieState(IPlayerAgent agent, IPlayerSwitchContext context) : base(agent, context)
        {
        }

        public override void OnEnter()
        {
            _agent.View.PlayDie();
        }
    }
}