using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Core.Components;
using Game.Runtime.Core.Damage;
using Game.Runtime.Core.FSM.Player;
using Game.Runtime.Core.FSM.Player.States;
using Game.Runtime.Data.Configs;
using Game.Runtime.Util.Extensions;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Runtime.Core.Enemies
{
    public class EnemyUnit : MonoBehaviour, IDamageTarget, IUnitAgent, IUnitSwitchContext
    {
        [field: SerializeField] public NavMeshMovingEngine Engine { get; private set; }
        [field: SerializeField] public CharacterView View { get; private set; }
        [field: SerializeField] public EnemyUnitConfig Config { get; private set; }
        
        public bool IsCreateClonesOnDeath => _isCreateClonesOnDeath;
        public EnemyUnit ClonePrefab => _cloneConfig.UnitPrefab;
        public int CloneCount => _cloneConfig.CloneCount;
        public bool IsAlive => _health.IsAlive;
        public int RewardPoints => Config.KillRewardPoints;

        IPlayerDamageTarget IUnitAgent.MyTarget => _myTarget;
        Transform IUnitAgent.MyTransform => transform;
        Vector3 IDamageTarget.Position => transform.position;

        [SerializeField] private Collider _collider;
        [Space, SerializeField] private bool _isCreateClonesOnDeath;
        [ShowIf("_isCreateClonesOnDeath"), SerializeField] private CloneConfig _cloneConfig;
       
        private HealthComponent _health;
        private IPlayerDamageTarget _myTarget;
        private List<BaseUnitState> _allStates;
        private BaseUnitState _currentState;

        public event Action<EnemyUnit> OnDieEvent;
        public event Action OnDamageEvent;

        private void Awake() 
        {
            _health = new HealthComponent(Config.MaxHealth); 

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

            Engine.Enabled();
            _health.Restore();    
            
            _collider.enabled = true;            

            _currentState = _allStates[0];
            _currentState.OnEnter();
        }

        void IDamageTarget.ApplyDamage(float damage)
        {
            _health.Value -= damage;

            if (_health.IsAlive)
            {
                OnDamageEvent?.Invoke();
                return;
            }           

            OnDieEvent?.Invoke(this);
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
            Engine.Disable();
            View.SetSpeed(0f);

            _collider.enabled = false;
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