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

        private bool _facingRight = true;
        private float _moveX;

        public void SetMove(float moveX)
        {
            _moveX = moveX;
        }

        private void Flip()
        {
            // switch the facing direction
            _facingRight = !_facingRight;
            Vector3 scale = _playerMesh.localScale;
            scale.x *= -1;
            _playerMesh.localScale = scale;
        }

        public void UpdateFacingDirection(float moveX)
        {

            // flip the player mesh based on movement direction
            if (moveX < 0 && _facingRight)
            {
                Flip();
            }
            else if (moveX > 0 && !_facingRight)
            {
                Flip();
            }
        }

        void FixedUpdate()
        {

            // safety checks
            if (_playerRb == null) return;
            //if (_moveX == 0) return;

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
