using UnityEngine;
using Game.Runtime.Util.Extensions;
using Game.Runtime.Data.Configs;
using GameRuntime.Data;

namespace Game.Runtime.Core.Components
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Animator _animator;

        private float curVelocity;
        private float _curAngle;

        public void RotateFromDirection(Vector3 dir, RotationConfig rotConfig)
        {
            if (dir == Vector3.zero) return;

            var angle = dir.GetUpAxisAngleRotate();
            _curAngle = Mathf.SmoothDampAngle(_curAngle, angle, ref curVelocity, rotConfig.Time, rotConfig.Speed);
            _body.rotation = Quaternion.Euler(0f, _curAngle, 0f);
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(GameConstants.Animation.SPEED, speed);
        }
    }
}