using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Core.UI
{
    public class MainHud : MonoBehaviour, IMainHUD
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _msgText;
        [SerializeField] BaseHud[] _subHuds;

        public event Action OnPressedRestartEvent;

        private void Awake() 
        {
            _restartButton.onClick.AddListener(() => OnPressedRestartEvent?.Invoke());    
        }
        
        void IMainHUD.Hide()
        {
            ElementsEnable(false);
        }

        void IMainHUD.ShowLossMsg()
        {
            ElementsEnable(true);
            _msgText.text = "YOU LOSS";
            HideAllSubHuds();
        }

        void IMainHUD.ShowWinMsg()
        {
            ElementsEnable(true);
            _msgText.text = "YOU WIN";
            HideAllSubHuds();
        }

        private void HideAllSubHuds()
        {
            Array.ForEach(_subHuds, hud => hud.Hide());
        }

        private void ElementsEnable(bool isActive)
        {
            _msgText.gameObject.SetActive(isActive);
            _restartButton.gameObject.SetActive(isActive);
        }
    }
}