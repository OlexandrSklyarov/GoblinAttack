
namespace Game.Runtime.Core.FSM.Player.States
{
    public class UnitAttackState : BaseUnitAttackState
    {        
        public UnitAttackState(IUnitAgent agent, IUnitSwitchContext context) : base(agent, context)
        {
        }

        public override void OnEnter()
        {            
            base.OnEnter();
            
            _agent.View.PlayAttack();
        }        
    }
}