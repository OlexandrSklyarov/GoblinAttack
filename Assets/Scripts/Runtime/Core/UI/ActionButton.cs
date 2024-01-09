using Game.Runtime.Util.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.UI
{
    public class ActionButton : MonoBehaviour
    {
        [SerializeField] private Image _bg;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fill;

        private void Awake() 
        {
            SetActiveProgress(1f);    
        }

        public void SetActiveProgress(float progress)
        {
            _fill.fillAmount = progress;
            _icon.color.SetAlpha(progress);
            _bg.color.SetAlpha(progress);
        }
    }
}
