using Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components References")]
        [SerializeField] private PlayerInputAdapter _inputAdapter;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerJump _playerJump;

        void OnEnable()
        {
            // safety check
            if (_inputAdapter == null) return;

            _inputAdapter.OnMoveX += _playerMovement.SetMove;
            _inputAdapter.OnMoveX += _playerMovement.UpdateFacingDirection;
            _inputAdapter.OnJump += _playerJump.RequestJump;
        }

        void OnDisable()
        {
            // safety check
            if (_inputAdapter == null) return;

            _inputAdapter.OnMoveX -= _playerMovement.SetMove;
            _inputAdapter.OnMoveX -= _playerMovement.UpdateFacingDirection;
            _inputAdapter.OnJump -= _playerJump.RequestJump;
        }

    }
}
