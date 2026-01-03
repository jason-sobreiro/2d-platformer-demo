using UnityEngine;

namespace Scripts.Player
{
    [DisallowMultipleComponent]
    public class PlayerAttack : MonoBehaviour
    {

        private bool _isAttacking = false;

        #region Unity Methods
        void Start()
        {
            
        }

        void Update()
        {
            if (_isAttacking)
            {
                // Attack logic here
                Debug.Log("Player is attacking!");
            }
        }

        #endregion

        #region Script Methods

        public void StartAttack()
        {
            _isAttacking = true;
        }
        
        public void StopAttack()
        {
            _isAttacking = false;
        }     

        #endregion
    }
}