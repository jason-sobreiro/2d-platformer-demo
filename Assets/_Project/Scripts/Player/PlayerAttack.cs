using Scripts.Managers;
using Scripts.States.Player;
using UnityEngine;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    public class PlayerAttack : MonoBehaviour
    {
        private bool _isAttacking = false;
        [SerializeField] private float _attackDuration = 0.25f;
        private float _attackTimer;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _gunBarrelRight;
        [SerializeField] private Transform _gunBarrelLeft;
        [SerializeField] private Transform _gunBarrelUpRight;
        [SerializeField] private Transform _gunBarrelUpLeft;
        private Transform _gunBarrel;
        private bool _isFacingRight = true;
        private bool _isAimingUpward = false;
        private bool _isWalking = false;

        #region Public Properties
        public bool IsAttacking => _isAttacking;
        public bool IsAimingUpward => _isAimingUpward;
        #endregion

        #region Unity Methods
        void Start()
        {
            _gunBarrel = _gunBarrelRight;
        }

        void Update()
        {

            _attackTimer += Time.deltaTime;

            if (_isAimingUpward && _isWalking)
            {
                return;
            }
            
            if (_isAttacking && _attackTimer >= _attackDuration)
                {

                    ShootProjectile();
                    _attackTimer = 0f;

                }
        }

        #endregion

        #region Script Methods

        public void SetAttackState()
        {
            _isAttacking = !_isAttacking;
        }

        public void ShootProjectile()
        {
            if (_projectilePrefab == null)
            {
                Debug.LogWarning("Projectile Prefab is not assigned in the inspector.");
                return;
            }

            GameObject projectile = Instantiate(_projectilePrefab, _gunBarrel.position, _gunBarrel.rotation);
            Projectile projScript = projectile.GetComponent<Projectile>();
            projScript.OnChangeDirection(_isFacingRight, _isAimingUpward);
            AudioManager.Instance.PlayAttackSFX();
        }



        public void UpdatingFacingDirection(float moveX)
        {

            _isWalking = Mathf.Abs(moveX) > 0.1f;

            if (moveX > 0f && _gunBarrel != _gunBarrelRight)
            {
                _isFacingRight = true;
                _gunBarrel = _gunBarrelRight;
                return;
            }

            if (moveX < 0f && _gunBarrel != _gunBarrelLeft)
            {
                _isFacingRight = false;
                _gunBarrel = _gunBarrelLeft;
                return;
            }
        }

        public void UpdatingLookDirection(float lookY)
        {

        

            if (lookY > 0.1f && _gunBarrel != _gunBarrelUpRight && _isFacingRight)
                {
                    _isAimingUpward = true;
                    _gunBarrel = _gunBarrelUpRight;
                    return;
                }

            if (lookY > 0.1f && _gunBarrel != _gunBarrelUpLeft && !_isFacingRight)
            {
                _isAimingUpward = true;
                _gunBarrel = _gunBarrelUpLeft;
                return;
            }

            if (lookY < 0.1f && _isAimingUpward)
            {
                _isAimingUpward = false;
                _gunBarrel = _isFacingRight ? _gunBarrelRight : _gunBarrelLeft;
                return;
            }
        }
        

        #endregion
    }
}