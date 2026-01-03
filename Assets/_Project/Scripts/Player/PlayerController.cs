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
    public class PlayerController : MonoBehaviour
    {
        #region Private Fields
        private PlayerInputAdapter _inputAdapter;
        private PlayerMovement _playerMovement;
        private PlayerJump _playerJump;
        private PlayerAttack _playerAttack;

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
            _inputAdapter.OnJump += _playerJump.RequestJump;
            _inputAdapter.OnStartAttack += _playerAttack.StartAttack;
            _inputAdapter.OnStopAttack += _playerAttack.StopAttack;
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
            _inputAdapter.OnJump -= _playerJump.RequestJump;
            _inputAdapter.OnStartAttack -= _playerAttack.StartAttack;
            _inputAdapter.OnStopAttack -= _playerAttack.StopAttack;
        }
        #endregion

        #region Script Methods

        void Init()
        {

            _inputAdapter = GetComponent<PlayerInputAdapter>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerJump = GetComponent<PlayerJump>();
            _playerAttack = GetComponent<PlayerAttack>();

        }

        #endregion

    }
}
