
using System;

namespace Game.Runtime.Core.UI
{
    public interface IMainHUD
    {
        void Hide();
        void ShowWinMsg();
        void ShowLossMsg();
        event Action OnPressedRestartEvent;
    }
}