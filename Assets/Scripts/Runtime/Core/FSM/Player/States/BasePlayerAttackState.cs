using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Core.Damage;
using Game.Runtime.Util.Extensions;
using UnityEngine;

namespace Game.Runtime.Core.FSM.Player.States
{
    public abstract class BasePlayerAttackState : BasePlayerState
    {
        private IDamageTarget _currentTarget;

        public BasePlayerAttackState(IPlayerAgent agent, IPlayerSwitchContext context) : base(agent, context)
        {            
        }

        public override void OnEnter()
        {            
            _agent.View.OnAttackExecuteEvent += AttackExecute;
            _agent.View.OnAttackCompletedEvent += AttackCompleted;
            _agent.OnDieEvent += OnDieState;
            _agent.OnDamageEvent += OnDamageState;
            
            FindTarget();            
        }

        public override void OnExit()
        {
            _agent.View.OnAttackExecuteEvent -= AttackExecute;
            _agent.View.OnAttackCompletedEvent -= AttackCompleted;
            _agent.OnDieEvent -= OnDieState;
            _agent.OnDamageEvent -= OnDamageState;

            _currentTarget = null;
        }        

        public override void OnUpdate()
        {   
            RotateToCurrentTarget();
        }

        private void AttackCompleted()
        {
            _context.SwitchState<PlayerIdleState>();
        }        

        private void FindTarget()
        {
            if (TryFindNearTarget(out IDamageTarget target))
            {
                _currentTarget = target;
            }
        }

        private bool TryFindNearTarget(out IDamageTarget target)
        {
            target = null;

            foreach(var curTarget in _agent.TargetSensor.Targets)
            {
                if (curTarget.IsAlive)
                {
                    target = curTarget;
                    return true;
                }   
            }

            return false;
        }

        private void AttackExecute()
        {
            if (_currentTarget != null && _currentTarget.IsAlive && IsTargetNear())
            {
                _currentTarget.ApplyDamage(_agent.Config.Attack.Damage);

                TryApplyDamageOtherTargets(_currentTarget);
            }
        }

        private void TryApplyDamageOtherTargets(IDamageTarget excludeTarget)
        {
            var targets = _agent.TargetSensor.FindTargetsInFront(excludeTarget, _agent.View.Forward, 
                _agent.Config.Attack.VisibleAngle);
            
            foreach (var target in targets)
            {
                var damage = _agent.Config.Attack.Damage * UnityEngine.Random.Range(0.1f, 0.5f);
                target?.ApplyDamage(damage);
            }           
        }
       
        private void RotateToCurrentTarget()
        {
            if (_currentTarget == null) return;
            
            var dir = (_currentTarget.Position - _agent.MyTransform.position).normalized;
            _agent.View.RotateFromDirection(dir, _agent.Config.Rotation);
        }

        private bool IsTargetNear()
        {
            return _currentTarget.Position.GetSqrDistanceXZ(_agent.MyTransform.position) <= 
                _agent.Config.Attack.Range * _agent.Config.Attack.Range;
        }        

        private void OnDieState()
        {
            _context.SwitchState<PlayerDieState>();
        }

        private void OnDamageState()
        {
            //Uncomment if you need to move into damage when attacking.
            //_context.SwitchState<PlayerDamageState>();
        }
    }
}