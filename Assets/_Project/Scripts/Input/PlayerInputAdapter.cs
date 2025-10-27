using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Scripts.Input
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputAdapter : MonoBehaviour
    {
        public event Action<float> OnMoveX;
        public event Action OnJump;

        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _jumpAction;

        // Handlers nomeados
        private void HandleMovePerformed(InputAction.CallbackContext context) 
            => OnMoveX?.Invoke(context.ReadValue<Vector2>().x);

        private void HandleMoveCanceled(InputAction.CallbackContext context) 
            => OnMoveX?.Invoke(0f);

        private void HandleJumpPerformed(InputAction.CallbackContext context) 
            => OnJump?.Invoke();

        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        void OnEnable()
        {
            var actions = _playerInput.actions;
            _moveAction = actions["Move"];
            _jumpAction = actions["Jump"];

            // subscribe to input events
            _moveAction.performed += HandleMovePerformed;
            _moveAction.canceled += HandleMoveCanceled;
            _moveAction.Enable();

            _jumpAction.performed += HandleJumpPerformed;
            _jumpAction.Enable();
        }

        void OnDisable()
        {
            // safety check
            if (_moveAction == null || _jumpAction == null) return;

            // unsubscribe from input events
            _moveAction.performed -= HandleMovePerformed;
            _moveAction.canceled -= HandleMoveCanceled;
            _moveAction.Disable();

            _jumpAction.performed -= HandleJumpPerformed;
            _jumpAction.Disable();
        }

        void OnDestroy()
        {
            OnMoveX = null;
            OnJump = null;
        }
    }
}
