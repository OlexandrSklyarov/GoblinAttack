using System;
using Game.Runtime.Core.Components;
using Game.Runtime.Data.Configs;
using Game.Runtime.Services;
using UnityEngine;

namespace Game.Runtime.Core.FSM.Player
{
    public interface IPlayerAgent
    {
        PhysicsMovingEngine Engine{ get; }
        CharacterView View{ get; }   
        PlayerConfig Config { get; }     
        CharacterStats Stats { get; }     

        event Action OnDieEvent;
        event Action OnDamageEvent;

        IInputService Input { get; }
        Transform MyTransform { get; }
    }
}