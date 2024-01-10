using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Core.Components;
using Game.Runtime.Core.Damage;
using Game.Runtime.Core.FSM.Player;
using Game.Runtime.Core.FSM.Player.States;
using Game.Runtime.Data.Configs;
using Game.Runtime.Services;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Core.Player
{
    public class PlayerController : MonoBehaviour, IPlayerSwitchContext, IPlayerAgent, 
        IPlayerDamageTarget, IPlayerChangeStats
    {
        [field: SerializeField] public Transform TargetPoint { get; private set; }
        [field: SerializeField] public PhysicsMovingEngine Engine { get; private set; }
        [field: SerializeField] public CharacterView View { get; private set; }
        [field: SerializeField] public PlayerConfig Config { get; private set; }
        [field: SerializeField] public HealthComponent Health { get; private set; }
        public CharacterStats Stats => _stats;
        public TargetSensor TargetSensor => _targetSensor;

        IInputService IPlayerAgent.Input => _input;
        Transform IPlayerAgent.MyTransform => transform;
        Vector3 IDamageTarget.Position => transform.position;
        bool IDamageTarget.IsAlive => Health.IsAlive;
        bool IPlayerAgent.IsCanUseSpecialAttack => _stats.SpecialAttackCooldown <= 0f && _targetSensor.IsTargetExist;

        private IInputService _input;
        private CharacterStats _stats;
        private TargetSensor _targetSensor;
        private List<BasePlayerState> _allStates;
        private BasePlayerState _currentState;

        public event Action OnDieEvent;
        public event Action OnDamageEvent;
        public event Action<float> RestoreSpecialAttackEvent;
        public event Action<float> ChangedHealthEvent;
        public event Action<bool> ChangeSpecialAttackStatusEvent;

        [Inject]
        private void Construct(IInputService input)
        {
            _input = input;
        }

        private void Awake()
        {
            _targetSensor = new TargetSensor(transform, Config.Attack);
            _stats = new CharacterStats();

            BindEvents();
            InitFSM();
        }

        private void BindEvents()
        {
            _stats.RestoreSpecialAttackEvent += (v) => RestoreSpecialAttackEvent?.Invoke(v);
            Health.ChangeValueEvent += (cur, max) => ChangedHealthEvent?.Invoke(cur / max);
            _targetSensor.ScanTargetResultEvent += (isActive) => ChangeSpecialAttackStatusEvent?.Invoke(isActive);
        }

        private void InitFSM()
        {
            _allStates = new List<BasePlayerState>()
            {
                new PlayerIdleState(this, this),
                new PlayerMoveState(this, this),
                new PlayerAttackState(this, this),
                new PlayerSpecialAttackState(this, this),
                new PlayerDamageState(this, this),
                new PlayerDieState(this, this)
            };

            _currentState = _allStates[0];
            _currentState.OnEnter();
        }

        private void FixedUpdate() 
        {
            _currentState?.OnFixedUpdate();           
        }

        private void Update() 
        {
            _targetSensor?.ScanTargets();            
            _currentState?.OnUpdate();
            _stats?.RestoreSpecialAttackCooldown();            
        }

        void IPlayerSwitchContext.SwitchState<T>()
        {
            var state = _allStates.FirstOrDefault(s => s is T);

            _currentState?.OnExit();
            _currentState = state;
            _currentState?.OnEnter();
        }
       
        void IDamageTarget.ApplyDamage(float damage)
        {
            Health.Value -= damage;

            if (Health.IsAlive)
            {
                OnDamageEvent?.Invoke();
                return;
            }

            OnDieEvent.Invoke();
        }
    }
}