using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Util.Extensions
{
    public static class UIExtensions
    {
        public static void SetAlpha(this Color color,  float normValue)
        {
            color.a = Mathf.Clamp01(normValue);
        }
    }
}