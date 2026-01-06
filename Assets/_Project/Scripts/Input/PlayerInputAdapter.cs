using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Scripts.Input
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputAdapter : MonoBehaviour
    {
        #region Events
        public event Action<float> OnMoveX;
        public event Action<float> OnLookY;
        public event Action OnJump;
        public event Action OnStartAttack;
        public event Action OnStopAttack;
        #endregion

        #region Private Fields
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;
        #endregion

        #region Handlers

        // Handlers
        private void HandleMovePerformed(InputAction.CallbackContext context)
            => OnMoveX?.Invoke(context.ReadValue<Vector2>().x);

        private void HandleLookPerformed(InputAction.CallbackContext context)
            => OnLookY?.Invoke(context.ReadValue<Vector2>().y);

        private void HandleMoveCanceled(InputAction.CallbackContext context)
            => OnMoveX?.Invoke(0f);

        private void HandleJumpPerformed(InputAction.CallbackContext context)
            => OnJump?.Invoke();

        private void HandleAttackPerformed(InputAction.CallbackContext context)
            => OnStartAttack?.Invoke();

        private void HandleAttackCanceled(InputAction.CallbackContext context)
            => OnStopAttack?.Invoke();

        #endregion

        #region Unity Methods

        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        void OnEnable()
        {
            var actions = _playerInput.actions;
            _moveAction = actions["Move"];
            _jumpAction = actions["Jump"];
            _attackAction = actions["Attack"];

            // subscribe to input events
            _moveAction.performed += HandleMovePerformed;
            _moveAction.canceled += HandleMoveCanceled;
            _moveAction.performed += HandleLookPerformed;
            _moveAction.canceled += HandleLookPerformed;
            _moveAction.Enable();

            _jumpAction.performed += HandleJumpPerformed;
            _jumpAction.Enable();

            _attackAction.performed += HandleAttackPerformed;
            _attackAction.canceled += HandleAttackCanceled;
            _attackAction.Enable();
        }

        void OnDisable()
        {
            // safety check
            if (_moveAction == null || _jumpAction == null) return;

            // unsubscribe from input events
            _moveAction.performed -= HandleMovePerformed;
            _moveAction.canceled -= HandleMoveCanceled;
            _moveAction.performed -= HandleLookPerformed;
            _moveAction.canceled -= HandleLookPerformed;
            _moveAction.Disable();

            _jumpAction.performed -= HandleJumpPerformed;
            _jumpAction.Disable();

            _attackAction.performed -= HandleAttackPerformed;
            _attackAction.canceled -= HandleAttackCanceled;
            _attackAction.Disable();
        }

        void OnDestroy()
        {
            OnMoveX = null;
            OnLookY = null;
            OnJump = null;
            OnStartAttack = null;
            OnStopAttack = null;
        }
        
        #endregion
    }
}
