using Game.Runtime.Data.Configs;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Runtime.Core.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMovingEngine : MonoBehaviour
    {
        private NavMeshAgent _navAgent;
        private MovingConfig _config;

        public float Speed => _navAgent.velocity.magnitude;

        public Vector3 Direction => _navAgent.velocity;

        private void Awake() 
        {
            _navAgent = GetComponent<NavMeshAgent>();    
        }

        public void Init(MovingConfig config)
        {
            _config = config;
        }

        public void MoveTo(Vector3 position)
        {
            _navAgent.isStopped = false;
            _navAgent.speed = _config.MaxSpeed;
            _navAgent.acceleration = _config.Acceleration;
            _navAgent.SetDestination(position);
        }

        public void Stop()
        {
            if (_navAgent == null) return;
            _navAgent.isStopped = true;
        }        
    }
}