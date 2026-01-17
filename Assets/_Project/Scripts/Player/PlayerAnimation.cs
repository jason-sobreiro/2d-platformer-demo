using UnityEngine;
using Scripts.States.Player;

public class PlayerAnimation : MonoBehaviour
{
    #region Private Fields
    private Animator _playerAnimator;
    private PlayerStates.States _currentState;

    #endregion

    #region Unity Methods

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _currentState = PlayerStates.CurrentState;
    }

    private void FixedUpdate()
    {
        // Observa mudanças de estado e sincroniza animações
        if (PlayerStates.CurrentState != _currentState)
        {
            _currentState = PlayerStates.CurrentState;
            PlayAnimation(_currentState);
        }
    }

    #endregion

    #region Script Methods

    private void PlayAnimation(PlayerStates.States state)
    {
        // safety check
        if (_playerAnimator == null)
        {
            Debug.LogWarning("Animator component is not assigned.");
            return;
        }

        _playerAnimator.Play(state.ToString());
    }

    #endregion
}
