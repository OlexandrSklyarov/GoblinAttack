using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Core.Components;
using Game.Runtime.Core.Damage;
using Game.Runtime.Core.FSM.Player;
using Game.Runtime.Core.FSM.Player.States;
using Game.Runtime.Data.Configs;
using Game.Runtime.Util.Extensions;
using UnityEngine;

namespace Game.Runtime.Core.Enemies
{
    public class EnemyUnit : MonoBehaviour, IDamageTarget, IUnitAgent, IUnitSwitchContext
    {
        [field: SerializeField] public NavMeshMovingEngine Engine { get; private set; }
        [field: SerializeField] public CharacterView View { get; private set; }
        [field: SerializeField] public EnemyUnitConfig Config { get; private set; }
        [field: SerializeField] public HealthComponent Health { get; private set; }
        public bool IsAlive => Health.IsAlive;

        IPlayerDamageTarget IUnitAgent.MyTarget => _myTarget;
        Transform IUnitAgent.MyTransform => transform;
        Vector3 IDamageTarget.Position => transform.position;
       
        private IPlayerDamageTarget _myTarget;
        private List<BaseUnitState> _allStates;
        private BaseUnitState _currentState;

        public event Action<EnemyUnit> OnDieEvent;
        public event Action OnDamageEvent;

        private void Awake() 
        {
            Engine.Init(Config.Moving);

            InitFSM();
        }

        private void InitFSM()
        {
            _allStates = new List<BaseUnitState>()
            {
                new UnitIdleState(this, this),
                new UnitMoveState(this, this),
                new UnitAttackState(this, this),
                new UnitDamageState(this, this),
                new UnitDieState(this, this)
            };            
        }

        public void Init(IPlayerDamageTarget target) 
        {
            _myTarget = target;
            Health.Restore();    

            _currentState = _allStates[0];
            _currentState.OnEnter();
        }

        void IDamageTarget.ApplyDamage(float damage)
        {
            Health.Value -= damage;

            if (Health.IsAlive)
            {
                OnDamageEvent?.Invoke();
                return;
            }

            OnDieEvent.Invoke(this);
        }

        public void OnUpdate()
        {                       
            _currentState?.OnUpdate(); 
        }

        public void OnFixedUpdate()
        {
            _currentState?.OnFixedUpdate(); 
        }

        public void Stop()
        {
            _currentState?.OnExit();
            Engine.Stop();
            View.SetSpeed(0f);
        }

        void IUnitSwitchContext.SwitchState<T>()
        {
            var state = _allStates.FirstOrDefault(s => s is T);

            _currentState?.OnExit();
            _currentState = state;
            _currentState?.OnEnter();
        }

        bool IUnitAgent.IsTargetNear()
        {
            return _myTarget.Position.GetSqrDistanceXZ(transform.position) <= 
                Config.Attack.Range * Config.Attack.Range;
        }
    }
}