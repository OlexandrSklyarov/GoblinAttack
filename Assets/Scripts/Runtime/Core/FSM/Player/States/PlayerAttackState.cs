
namespace Game.Runtime.Core.FSM.Player.States
{
    public class PlayerAttackState : BasePlayerAttackState
    {        
        public PlayerAttackState(IPlayerAgent agent, IPlayerSwitchContext context) : base(agent, context)
        {
        }

        public override void OnEnter()
        {            
            base.OnEnter();
            
            _agent.View.PlayAttack();
        }        
    }
}