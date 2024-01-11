using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.Runtime.Core.UI
{
    public class LineBarView : MonoBehaviour
    {        
        [SerializeField] private Image _fastBar;
        [SerializeField] private Image _slowBar; 
        [SerializeField, Min(0.01f)] private float _changeValueAnimationDuration = 0.5f;

        private Tween _barTween;   

        public void SetProgress(float normValue)
        {
            var value = Mathf.Clamp01(normValue);     

            _barTween.Complete();

            _barTween = _fastBar.DOFillAmount(value, _changeValueAnimationDuration)
                .SetLink(_fastBar.gameObject)
                .OnComplete(() => 
                    _slowBar.DOFillAmount(value, _changeValueAnimationDuration)
                    .SetLink(_slowBar.gameObject));
        }        

        private void OnDisable() 
        {
            _barTween?.Kill();
        }
    }
}
