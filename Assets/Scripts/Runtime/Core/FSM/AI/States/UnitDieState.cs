
namespace Game.Runtime.Core.FSM.Player.States
{
    public class UnitDieState : BaseUnitState
    {
        public UnitDieState(IUnitAgent agent, IUnitSwitchContext context) : base(agent, context)
        {
        }

        public override void OnEnter()
        {
            _agent.Engine.Stop();
            _agent.View.PlayDie();
            _agent.ReclaimAsync();
        }
    }
}