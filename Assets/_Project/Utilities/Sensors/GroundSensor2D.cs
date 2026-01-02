using UnityEngine;

namespace Scripts.Utilities.Sensors
{
    [DisallowMultipleComponent]
    public class GroundSensor2D : MonoBehaviour
    {
        [SerializeField] Transform groundCheckPoint;
        [SerializeField] float groundCheckRadius = 0.1f;
        [SerializeField] LayerMask whatIsGround;

        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround) != null;
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (groundCheckPoint == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
#endif

    }
}
