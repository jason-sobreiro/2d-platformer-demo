using System.ComponentModel;
using Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInputAdapter))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerJump))]
    [RequireComponent(typeof(PlayerAttack))]
    [RequireComponent(typeof(PlayerAnimation))]
    public class PlayerController : MonoBehaviour
    {
        #region Private Fields
        private PlayerInputAdapter _inputAdapter;
        private PlayerMovement _playerMovement;
        private PlayerJump _playerJump;
        private PlayerAttack _playerAttack;
        private PlayerAnimation _playerAnimation;

        #endregion

        #region Unity Methods

        void Awake()
        {
            Init();
        }

        void OnEnable()
        {
            // safety check
            if (_inputAdapter == null)
            {
                return;
            }

            _inputAdapter.OnMoveX += _playerMovement.SetMove;
            _inputAdapter.OnMoveX += _playerMovement.UpdateFacingDirection;
            _inputAdapter.OnMoveX += _playerAttack.UpdatingFacingDirection;
            _inputAdapter.OnLookY += _playerAttack.UpdatingLookDirection;
            _inputAdapter.OnLookY += HandleLookDirection;
            _inputAdapter.OnJump += _playerJump.RequestJump;
            _inputAdapter.OnStartAttack += _playerAttack.SetAttackState;
            _inputAdapter.OnStartAttack += _playerAnimation.SetAttackState;
            _inputAdapter.OnStopAttack += _playerAttack.SetAttackState;
            _inputAdapter.OnStopAttack += _playerAnimation.SetAttackState;
        }

        void OnDisable()
        {
            // safety check
            if (_inputAdapter == null)
            {
                return;
            }

            _inputAdapter.OnMoveX -= _playerMovement.SetMove;
            _inputAdapter.OnMoveX -= _playerMovement.UpdateFacingDirection;
            _inputAdapter.OnMoveX -= _playerAttack.UpdatingFacingDirection;
            _inputAdapter.OnLookY -= _playerAttack.UpdatingLookDirection;
            _inputAdapter.OnLookY -= HandleLookDirection;
            _inputAdapter.OnJump -= _playerJump.RequestJump;
            _inputAdapter.OnStartAttack -= _playerAttack.SetAttackState;
            _inputAdapter.OnStartAttack -= _playerAnimation.SetAttackState;
            _inputAdapter.OnStopAttack -= _playerAttack.SetAttackState;
            _inputAdapter.OnStopAttack -= _playerAnimation.SetAttackState;
        }
        #endregion

        #region Script Methods

        void Init()
        {

            _inputAdapter = GetComponent<PlayerInputAdapter>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerJump = GetComponent<PlayerJump>();
            _playerAttack = GetComponent<PlayerAttack>();
            _playerAnimation = GetComponent<PlayerAnimation>();

        }

        void HandleLookDirection(float lookY)
        {
            _playerAnimation.SetAimingUpwardState(lookY > 0.1f);
        }

        #endregion

    }
}
