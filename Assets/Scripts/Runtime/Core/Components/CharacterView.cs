using UnityEngine;
using Game.Runtime.Util.Extensions;
using Game.Runtime.Data.Configs;
using GameRuntime.Data;
using System;
using Game.Runtime.Core.Animations;

namespace Game.Runtime.Core.Components
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterAnimationProvider _animationProvider;

        private float curVelocity;
        private float _curAngle;
        private int _animSpeedPRM;
        private int _animAttackPRM;
        private int _animSpecialAttackPRM;
        private int _animDiePRM;
        private int _animDamagePRM;

        public event Action OnAttackExecuteEvent;
        public event Action OnAttackCompletedEvent;
        public event Action OnDamageCompletedEvent;
        
        private void Awake() 
        {
            _animSpeedPRM = Animator.StringToHash(GameConstants.Animation.SPEED);    
            _animAttackPRM = Animator.StringToHash(GameConstants.Animation.ATTACK);    
            _animSpecialAttackPRM = Animator.StringToHash(GameConstants.Animation.SPECIAL_ATTACK);    
            _animDiePRM = Animator.StringToHash(GameConstants.Animation.DIE);  
            _animDamagePRM = Animator.StringToHash(GameConstants.Animation.DAMAGE);  

            _animationProvider.OnAttackExecuteEvent += () => OnAttackExecuteEvent?.Invoke();   
            _animationProvider.OnAttackCompleteEvent += () => OnAttackCompletedEvent?.Invoke();   
            _animationProvider.OnDamageCompleteEvent += () => OnDamageCompletedEvent?.Invoke();   
        }

        public void RotateFromDirection(Vector3 dir, RotationConfig rotConfig)
        {
            if (dir == Vector3.zero) return;

            var angle = dir.GetUpAxisAngleRotate();
            _curAngle = Mathf.SmoothDampAngle(_curAngle, angle, ref curVelocity, rotConfig.Time, rotConfig.Speed);
            _body.rotation = Quaternion.Euler(0f, _curAngle, 0f);
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(_animSpeedPRM, speed);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(_animAttackPRM);
        }

        public void PlaySpecialAttack()
        {
            _animator.SetTrigger(_animSpecialAttackPRM);
        }

        public void PlayDie()
        {
            _animator.SetTrigger(_animDiePRM);
        }

        public void PlayDamage()
        {
            _animator.SetTrigger(_animDamagePRM);
        }

        internal void RotateFromDirection(Vector3 moveDirection, object rotation)
        {
            throw new NotImplementedException();
        }
    }
}