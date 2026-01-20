using Scripts.Managers;
using Scripts.Utilities.Sensors;
using UnityEngine;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    public class PlayerJump : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private GroundSensor2D _groundSensor;
        [SerializeField] private float _fallGravityMultiplier = 2.5f;

        #endregion

        #region Private Fields
        private Rigidbody2D _playerRb;
        private bool _jumpRequested = false;
        private float _defaultGravityScale;

        #endregion

        #region Unity Methods
        private void Start()
        {

            _playerRb = GetComponent<Rigidbody2D>();

            // Store the default gravity scale
            _defaultGravityScale = _playerRb.gravityScale;

            // Ensure a strong enough gravity for snappy jumps
            if (_defaultGravityScale < 2f)
            {
                _defaultGravityScale = 3f;
                _playerRb.gravityScale = _defaultGravityScale;
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

        void OnValidate()
        {
            if (_playerRb == null)
            {
                _playerRb = GetComponent<Rigidbody2D>();
            }
        }

        #endregion

        #region Script Methods
        public void RequestJump()
        {
            // Only request a jump if we're grounded (prevents queued jumps while airborne).
            // If there's no GroundSensor assigned, fall back to allowing the request.
            if (_groundSensor == null || _groundSensor.IsGrounded())
            {
                _jumpRequested = true;
            }
        }

        void PerformJump()
        {
            if (_jumpRequested)
            {
                Vector3 velocity = _playerRb.linearVelocity;
                velocity.y = _jumpForce;
                _playerRb.linearVelocity = velocity;
                _jumpRequested = false;
                AudioManager.Instance.PlayJumpSFX();
            }
        }

        public bool IsGrounded()
        {
            if (_groundSensor != null)
            {
                return _groundSensor.IsGrounded();
            }
            return false;
        }

        #endregion
        
    }
}
