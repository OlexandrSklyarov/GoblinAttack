using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Core.UI
{
    public class LineBarView : MonoBehaviour
    {        
        [SerializeField] private Image _fill;

        public void SetProgress(float normValue)
        {
            var value = Mathf.Clamp01(normValue);
            _fill.fillAmount = value;
        }
    }
}
