using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Runtime.Util.Extensions;

namespace Game.Runtime.UI
{
    public class ActionButton : MonoBehaviour, IPointerDownHandler
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
            _icon.SetAlpha(progress);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one * 0.7f,  0.05f)
                .OnComplete(() => transform.DOScale(Vector3.one,  0.05f));
        }
    }
}
