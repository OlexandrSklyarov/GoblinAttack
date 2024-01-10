using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Runtime.Util.Extensions;
using System;

namespace Game.Runtime.UI
{
    public class ActionButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _bg;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fill;
        private bool _isButtonInteraction;

        private void Awake() 
        {
            _isButtonInteraction = true;
            SetActiveProgress(1f);    
        }

        public void SetActiveProgress(float progress)
        {
            _fill.fillAmount = progress;            
        }

        public void SetActiveInteraction(bool isActive)
        {
            _icon.SetAlpha((isActive)? 1f : 0.1f);
            _isButtonInteraction = isActive;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!_isButtonInteraction) return;
            
            transform.DOScale(Vector3.one * 0.7f,  0.05f)
                .OnComplete(() => transform.DOScale(Vector3.one,  0.05f));
        }
    }
}
