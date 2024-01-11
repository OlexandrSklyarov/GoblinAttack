using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Runtime.Core.UI
{
    public abstract class BaseHud : MonoBehaviour
    {    
        [SerializeField] protected LineBarView _hpBar;
        [SerializeField] protected TextMeshProUGUI _waveText; 

        protected void OnChangeWaveProgress(int curWave, int wavesCount)
        {
            _waveText.text = $"WAVES: {curWave}/{wavesCount}";
            _waveText.transform.DOPunchScale(Random.insideUnitCircle * 0.9f, 0.1f);
        }

        protected void OnChangeHealth(float progress)
        {
            Debug.Log("OnChangeHealth");
            _hpBar.SetProgress(progress);
        }

        public void Hide() => gameObject.SetActive(false);   
    }
}