using Scripts.Utilities.Sensors;
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

        private bool _jumpRequested = false;

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
            // check if jump was requested and player is grounded
            if (_jumpRequested && (_groundSensor == null || _groundSensor.IsGrounded()))
            {
                Vector3 velocity = _playerRb.linearVelocity;
                velocity.y = _jumpForce;
                _playerRb.linearVelocity = velocity;
                _jumpRequested = false;
            }
        }
    }
}
