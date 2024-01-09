
namespace Game.Runtime.Core.FSM.Player
{
    [System.Serializable]
    public abstract class BasePlayerState
    {
        protected readonly IPlayerAgent _agent;
        protected readonly IPlayerSwitchContext _context;

        public BasePlayerState(IPlayerAgent agent, IPlayerSwitchContext context)
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