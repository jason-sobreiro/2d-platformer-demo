using Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components References")]
        [SerializeField] private Rigidbody2D playerRb;
        [SerializeField] private PlayerInput gameInput;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;

        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Transform groundCheck;

        [Header("Graphics Settings")]
        [SerializeField] private GameObject playerMesh;

        // cached inputs
        private InputAction _moveInput;
        private InputAction _jumpInput;

        // input states
        private float _moveX;
        private bool _jumpPressed;

        // facet direction
        private bool _facingRight = true;
        private Vector2 _playerScale;

        void Awake()
        {
            if (playerRb == null)
            {
                playerRb = GetComponent<Rigidbody2D>();
            }

            if (gameInput == null)
            {
                gameInput = GetComponent<PlayerInput>();
            }

            _playerScale = playerMesh.transform.localScale;
        }

        void OnEnable()
        {
            // get input actions
            var inputActions = gameInput.actions;
            _moveInput = inputActions["Move"];
            _jumpInput = inputActions["Jump"];

            // subscribe to input events
            _moveInput.performed += OnMove;
            _moveInput.canceled += OnMove;
            _moveInput.Enable();

            _jumpInput.performed += OnJump;
            _jumpInput.Enable();

        }

        void OnDisable()
        {
            // safety check
            if (_moveInput == null || _jumpInput == null) return;

            // unsubscribe from input events
            _moveInput.performed -= OnMove;
            _moveInput.canceled -= OnMove;
            _moveInput.Disable();

            _jumpInput.performed -= OnJump;
            _jumpInput.Disable();
        }

        void OnMove(InputAction.CallbackContext context)
        {
            _moveX = context.ReadValue<Vector2>().x;
            
            // handle sprite facing direction
            if ((_moveX > 0 && !_facingRight) || (_moveX < 0 && _facingRight))
            {
                Flip();
            }
            
        }

        void Flip()
        {
            _facingRight = !_facingRight;
            _playerScale.x *= -1;
            playerMesh.transform.localScale = _playerScale;
        }
        void OnJump(InputAction.CallbackContext context) => _jumpPressed = true;

        void FixedUpdate()
        {
            // handle horizontal movement
            Vector2 velocity = playerRb.linearVelocity;
            velocity.x = _moveX * moveSpeed;
            playerRb.linearVelocity = velocity;

            // handle jumping
            if (_jumpPressed && IsGrounded())
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            // reset jump state
            _jumpPressed = false;
        }

        bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround) != null;
    }

}
