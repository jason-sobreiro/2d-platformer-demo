using UnityEngine;
using Scripts.AnimationStates.Player;

public class PlayerAnimation : MonoBehaviour
{


    #region Private Fields
    private Animator _playerAnimator;
    private Rigidbody2D _playerRb;
    private bool _isAttacking = false;

    private PlayerStates.AnimationState _currentState;

    private const float speedYThreshold = 0.1f;
    private const float speedXThreshold = 0.01f;

    #endregion

    #region Unity Methods

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleAnimationStateChange();
    }

    #endregion

    #region Script Methods



    private void HandleAnimationStateChange()
    {
        var state = ResolveState();
        SetAnimationState(state);
    }

    private PlayerStates.AnimationState ResolveState()
    {
        // jumping / falling
        if (IsAirborne())
        {
            return PlayerStates.AnimationState.Jump;
        }

        // idle / stand attack
        if (IsIdle())
        {
            return _isAttacking ? PlayerStates.AnimationState.Attack
                                : PlayerStates.AnimationState.Idle;
        }

        // running / run attack
        return _isAttacking ? PlayerStates.AnimationState.Run_Attack
                            : PlayerStates.AnimationState.Run;
    }

    private bool IsAirborne()
    {
        return _playerRb.linearVelocityY > speedYThreshold ||
        _playerRb.linearVelocityY < -speedYThreshold;
    }

    private bool IsIdle()
    {
        return Mathf.Abs(_playerRb.linearVelocityX) < speedXThreshold;
    }

    private void SetAnimationState(PlayerStates.AnimationState newState)
    {

        // safety checks
        if (_playerAnimator == null)
        {
            Debug.LogWarning("Animator component is not assigned.");
            return;
        }

        if (_currentState == newState)
        {
            return;
        }

        _currentState = newState;
        _playerAnimator.Play(newState.ToString());
    }

    public void SetAttackState()
    {
        _isAttacking = !_isAttacking;
    }



    #endregion
}
