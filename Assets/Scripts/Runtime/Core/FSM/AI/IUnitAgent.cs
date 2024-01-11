using System;
using Game.Runtime.Core.Components;
using Game.Runtime.Core.Damage;
using Game.Runtime.Core.Enemies;
using Game.Runtime.Data.Configs;
using UnityEngine;

namespace Game.Runtime.Core.FSM.Player
{
    public interface IUnitAgent
    {
        NavMeshMovingEngine Engine{ get; }
        CharacterView View{ get; }   
        EnemyUnitConfig Config { get; }      
        IPlayerDamageTarget MyTarget { get; }
        Transform MyTransform { get; }

        event Action<EnemyUnit> OnDieEvent;
        event Action OnDamageEvent;

        bool IsTargetNear();
    }
}