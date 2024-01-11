using System;
using Game.Runtime.Core.Components;
using Game.Runtime.Data.Configs;
using Game.Runtime.Services;
using UnityEngine;

namespace Game.Runtime.Core.FSM.Player
{
    public interface IPlayerAgent
    {
        TargetSensor TargetSensor {get;}
        PhysicsMovingEngine Engine{ get; }
        CharacterView View{ get; }   
        PlayerConfig Config { get; }     
        CharacterStats Stats { get; }     
        IInputService Input { get; }
        Transform MyTransform { get; }
        bool IsCanUseSpecialAttack { get; }
        bool IsAlive { get; }

        event Action OnDieEvent;
        event Action OnDamageEvent;
    }
}