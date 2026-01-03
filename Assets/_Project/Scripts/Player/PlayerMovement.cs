using UnityEngine;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Transform _playerMesh;

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

        #endregion
        
    }
}
