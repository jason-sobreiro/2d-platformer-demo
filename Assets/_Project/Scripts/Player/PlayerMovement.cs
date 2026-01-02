using UnityEngine;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _playerRb;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Transform _playerMesh;

        private Vector3 _meshScale;

        private bool _facingRight = true;
        private float _moveX;

        private void Awake()
        {
            // store the initial scale for flipping
            if (_playerMesh != null)
            {
                _meshScale = _playerMesh.localScale;
            }
        }

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

        void FixedUpdate()
        {

            // safety checks
            if (_playerRb == null) return;

            Vector3 velocity = _playerRb.linearVelocity;
            velocity.x = _moveX * _moveSpeed;
            _playerRb.linearVelocity = velocity;
        }

        // Editor-time helper: auto-assign Rigidbody2D when possible
        void OnValidate()
        {
            if (_playerRb == null)
            {
                _playerRb = GetComponent<Rigidbody2D>();
            }
        }

    }
}
