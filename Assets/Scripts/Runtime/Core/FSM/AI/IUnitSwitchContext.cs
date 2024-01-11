
namespace Game.Runtime.Core.FSM.Player
{
    public interface IUnitSwitchContext
    {        
        void SwitchState<T>() where T : BaseUnitState;
    }
}