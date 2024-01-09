
namespace Game.Runtime.Core.FSM.Player
{
    public interface IPlayerSwitchContext
    {        
        void SwitchState<T>() where T : BasePlayerState;
    }
}