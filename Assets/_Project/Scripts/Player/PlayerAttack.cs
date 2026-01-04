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
        private Transform _gunBarrel;
        private bool _isFacingRight = true;

        #region Unity Methods
        void Start()
        {
            _gunBarrel = _gunBarrelRight;
        }

        void Update()
        {

            _attackTimer += Time.deltaTime;
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
            projScript.OnChangeDirection(_isFacingRight);

        }



        public void UpdatingFacingDirection(float moveX)
        {

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
        
        
        

        #endregion
    }
}