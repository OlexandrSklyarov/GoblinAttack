
namespace Game.Runtime.Core.FSM.Player.States
{
    public class PlayerSpecialAttackState : BasePlayerAttackState
    {        
        public PlayerSpecialAttackState(IPlayerAgent agent, IPlayerSwitchContext context) : base(agent, context)
        {
        }

        public override void OnEnter()
        {            
            base.OnEnter();

            _agent.Stats.AddSpecialAttackCooldown(_agent.Config.Attack.SuperAttackCooldown);
            _agent.View.PlaySpecialAttack();
        }        
    }
}