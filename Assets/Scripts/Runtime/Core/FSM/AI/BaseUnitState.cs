
namespace Game.Runtime.Core.FSM.Player
{
    [System.Serializable]
    public abstract class BaseUnitState
    {
        protected readonly IUnitAgent _agent;
        protected readonly IUnitSwitchContext _context;

        public BaseUnitState(IUnitAgent agent, IUnitSwitchContext context)
        {
            _agent = agent;
            _context = context;
        }

        public virtual void OnEnter(){}
        public virtual void OnUpdate(){}
        public virtual void OnFixedUpdate(){}
        public virtual void OnExit(){}
    }
}