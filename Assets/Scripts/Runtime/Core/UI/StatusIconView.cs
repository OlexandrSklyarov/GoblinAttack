using Game.Runtime.Util.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Core.UI
{
    public class StatusIconView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fill;

        private void Awake() 
        {
            SetActiveProgress(1f);    
        }

        public void SetActiveProgress(float progress)
        {
            _fill.fillAmount = progress;            
        }

        public void SetActiveInteraction(bool isActive)
        {
            _icon.SetAlpha((isActive)? 1f : 0.1f);
        }
    }
}