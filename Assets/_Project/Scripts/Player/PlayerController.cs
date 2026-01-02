using Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInputAdapter))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerJump))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components References")]
        [SerializeField] private PlayerInputAdapter _inputAdapter;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerJump _playerJump;

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
        }

        void Init()
        {
            // auto-assign components when possible
            if (_inputAdapter == null)
            {
                _inputAdapter = GetComponent<PlayerInputAdapter>();
            }

            if (_playerMovement == null)
            {
                _playerMovement = GetComponent<PlayerMovement>();
            }
            
            if (_playerJump == null)
            {
                _playerJump = GetComponent<PlayerJump>();
            }
        }

    }
}
