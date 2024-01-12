using UnityEngine;

namespace Game.Runtime.Util.Extensions
{
    public static class TimeExtensions
    {
        public static bool IsTimeEnd(ref float timer, float delay)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                return false;
            }

            timer = delay;
            return true;    
        }
    }
}