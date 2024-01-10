using TMPro;
using UnityEngine;

namespace Game.Runtime.Core.UI
{
    public class BaseHud : MonoBehaviour
    {    
        [SerializeField] protected LineBarView _hpBar;
        [SerializeField] private TextMeshProUGUI _waveText;  
        public void Hide() => gameObject.SetActive(false);
    }
}