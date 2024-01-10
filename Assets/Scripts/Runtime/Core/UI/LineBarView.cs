using UnityEngine;

namespace Game.Runtime.Core.UI
{
    public class LineBarView : MonoBehaviour
    {
        private enum Orientation {Vertical, Horizontal}

        [SerializeField] private Orientation _orientation;
        [SerializeField] private Transform _bar;

        public void SetProgress(float normValue)
        {
            var value = Mathf.Clamp01(normValue);
            var scale = _bar.localScale;

            if (_orientation == Orientation.Vertical)
            {
                scale.y = value;
            }
            else
            {
                scale.x = value;
            }

            _bar.localScale = scale;
        }
    }
}
