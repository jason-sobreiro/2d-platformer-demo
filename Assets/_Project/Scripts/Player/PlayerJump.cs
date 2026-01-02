using System;
using Scripts.Utilities.Sensors;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _playerRb;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private GroundSensor2D _groundSensor;
        [SerializeField] private float _fallGravityMultiplier = 2.5f;

        private bool _jumpRequested = false;
        private float _defaultGravityScale;

        private void Start()
        {
            // Store the default gravity scale
            _defaultGravityScale = _playerRb.gravityScale;
        }

        public void RequestJump()
        {
            // Only request a jump if we're grounded (prevents queued jumps while airborne).
            // If there's no GroundSensor assigned, fall back to allowing the request.
            if (_groundSensor == null || _groundSensor.IsGrounded())
            {
                _jumpRequested = true;
            }
        }

        void FixedUpdate()
        {
            // Apply gravity multiplier based on current velocity state
            float targetGravityScale = _playerRb.linearVelocity.y < 0 ? _defaultGravityScale * _fallGravityMultiplier : _defaultGravityScale;
            
            if (_playerRb.gravityScale != targetGravityScale)
            {
                _playerRb.gravityScale = targetGravityScale;
            }

            PerformJump();
        }

        void PerformJump()
        {
            if (_jumpRequested)
            {
                Vector3 velocity = _playerRb.linearVelocity;
                velocity.y = _jumpForce;
                _playerRb.linearVelocity = velocity;    
                _jumpRequested = false;
            }
        }
    }
}
