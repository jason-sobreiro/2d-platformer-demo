using UnityEngine;
using Scripts.AnimationStates.Player;

public class PlayerAnimation : MonoBehaviour
{


    #region Private Fields
    private Animator _playerAnimator;
    private Rigidbody2D _playerRb;
    private bool _isAttacking = false;
    private bool _isAimingUpward = false;

    private PlayerStates.States _currentState;

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

    private PlayerStates.States ResolveState()
    {
        // jumping / falling
        if (IsAirborne())
        {
            return PlayerStates.States.Jump;
        }

        // idle / stand attack / attack up
        if (IsIdle())
        {
            if (_isAttacking && _isAimingUpward)
            {
                return PlayerStates.States.Attack_Up;
            }
            return _isAttacking ? PlayerStates.States.Attack
                                : PlayerStates.States.Idle;
        }

        // running / run attack
        return _isAttacking ? PlayerStates.States.Run_Attack
                            : PlayerStates.States.Run;
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

    private void SetAnimationState(PlayerStates.States newState)
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


    public void SetAimingUpwardState(bool isAiming)
    {
        _isAimingUpward = isAiming;
    }


    #endregion
}
