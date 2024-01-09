using Game.Runtime.Data.Configs;
using UnityEngine;

namespace Game.Runtime.Core.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovingEngine : MonoBehaviour
    {
        public float Speed => new Vector2(_rb.velocity.x, _rb.velocity.z).magnitude;
        
        private Rigidbody _rb;


        private void Awake() 
        {
            _rb = GetComponent<Rigidbody>();   
        }

        public void Move(Vector3 dir, MovingConfig config)
        {
            _rb.AddForce(dir * config.Acceleration, ForceMode.Acceleration);

            ClampVelocity(config.MaxSpeed);
        }

        private void ClampVelocity(float speed)
        {
            var curVelocity = _rb.velocity;
            var horVelocity = new Vector3(curVelocity.x, 0f, curVelocity.z);

            if (horVelocity.sqrMagnitude <= speed * speed) return;

            var limitedVelocity = horVelocity.normalized * speed;            
            _rb.velocity = new Vector3(limitedVelocity.x, curVelocity.y, limitedVelocity.z);
        }
    }
}