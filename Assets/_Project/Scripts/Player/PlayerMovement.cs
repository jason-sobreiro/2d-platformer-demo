using Scripts.Managers;
using Scripts.Utilities.Sensors;
using UnityEngine;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Transform _playerMesh;

        [SerializeField] private float _walkCycleSpeed = 0.3f;
        private float _walkCycleTimer = 0f;

        private GroundSensor2D _groundSensor;

        #endregion

        #region Private Fields

        private Rigidbody2D _playerRb;
        private Vector3 _meshScale;

        private bool _facingRight = true;
        private float _moveX;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _playerRb = GetComponent<Rigidbody2D>();
            _groundSensor = GetComponent<GroundSensor2D>();

            if (_playerMesh != null)
            {
                _meshScale = _playerMesh.localScale;
            }
        }

        void FixedUpdate()
        {

            // safety check
            if (_playerRb == null)
            {
                return;
            }

            Vector3 velocity = _playerRb.linearVelocity;
            velocity.x = _moveX * _moveSpeed;
            _playerRb.linearVelocity = velocity;
            _walkCycleTimer += Time.fixedDeltaTime;

            PlayWalkCycleSound();

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

        public void SetMove(float moveX)
        {
            _moveX = moveX;
        }

        private void Flip()
        {
            // switch the facing direction
            _facingRight = !_facingRight;
            _meshScale.x *= -1;
            _playerMesh.localScale = _meshScale;
        }

        public void UpdateFacingDirection(float moveX)
        {

            // flip the player mesh based on movement direction
            bool movingLeft = moveX < 0 && _facingRight;
            bool movingRight = moveX > 0 && !_facingRight;

            if (movingLeft || movingRight)
            {
                Flip();
            }

        }
        
        private void PlayWalkCycleSound()
        {
            
            if(_groundSensor == null || !_groundSensor.IsGrounded())
            {
                return;
            }

            if (Mathf.Abs(_moveX) > 0.1f && _walkCycleTimer > _walkCycleSpeed)
            {
                _walkCycleTimer = 0f;
                AudioManager.Instance.PlayWalkSFX();

            }
            
            
        }
        


        #endregion

    }
}
