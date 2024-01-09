using System.Linq;
using System.Threading.Tasks;
using Game.Runtime.Core.Damage;
using Game.Runtime.Util.Extensions;
using UnityEngine;

namespace Game.Runtime.Core.FSM.Player.States
{
    public abstract class BasePlayerAttackState : BasePlayerState
    {
        private Collider[] _targets;
        private IDamageTarget _currentTarget;

        public BasePlayerAttackState(IPlayerAgent agent, IPlayerSwitchContext context) : base(agent, context)
        {
            _targets = new Collider[10];
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
            if (TryFindTarget(out IDamageTarget target))
            {
                _currentTarget = target;
            }
        }

        private void AttackExecute()
        {
            if (_currentTarget != null && _currentTarget.IsAlive)
            {
                _currentTarget.ApplyDamage(_agent.Config.Attack.Damage);
            }
        }

        private void RotateToCurrentTarget()
        {
            if (_currentTarget == null) return;
            
            var dir = (_currentTarget.Position - _agent.MyTransform.position).normalized;
            _agent.View.RotateFromDirection(dir, _agent.Config.Rotation);
        }

        private bool TryFindTarget(out IDamageTarget target)
        {
            target = null;

            var count = Physics.OverlapSphereNonAlloc
            (
                _agent.MyTransform.position,
                _agent.Config.Attack.Range,
                _targets,
                _agent.Config.Attack.TargetLayer
            );

            if (count <= 0) return false;

            var allTargets = _targets.Where(x => x != null)
                .OrderBy(x => _agent.MyTransform.position.GetDistanceXZ(x.transform.position))
                .ToArray();

            for (int i = 0; i < allTargets.Length; i++)
            {
                if (allTargets[i].TryGetComponent(out target) && target.IsAlive)
                {
                    return true;
                }            
            }

            return false;
        }

        private void OnDieState()
        {
            _context.SwitchState<PlayerDieState>();
        }

        private void OnDamageState()
        {
            _context.SwitchState<PlayerDamageState>();
        }
    }
}