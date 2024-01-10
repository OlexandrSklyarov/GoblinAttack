using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Util.Extensions
{
    public static class UIExtensions
    {
        public static void SetAlpha(this Image image,  float normValue)
        {
            var color = image.color;
            color.a = Mathf.Clamp01(normValue);
            image.color = color;
        }
    }
}