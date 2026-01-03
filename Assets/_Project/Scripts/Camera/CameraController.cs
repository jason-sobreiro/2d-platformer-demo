using System;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Camera
{
    [DisallowMultipleComponent]
    public class CameraController : MonoBehaviour
    {

        #region Fields
        [SerializeField] private Transform player;

        [SerializeField] private float cameraSpeed = 3f;

        [SerializeField] private float offsetAmount = 5f;
        [SerializeField] private float smoothTime = 3f;

        #endregion

        #region Private Fields
        private float offset;
        private Vector3 targetPosition = Vector3.zero;

        private Vector3 _lastPlayerPosition;

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (player != null)
            {
                targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
                _lastPlayerPosition = player.position;
            }
        }

        void LateUpdate()
        {
            // safety check
            if (player == null)
            {
                return;
            }

            // Calculate player's horizontal movement direction from position change
            float horizontalDelta = player.position.x - _lastPlayerPosition.x;
            _lastPlayerPosition = player.position;

            if (horizontalDelta > 0.01f)
            {
                offset = Mathf.Lerp(offset, offsetAmount, smoothTime * Time.deltaTime);
            }
            else if (horizontalDelta < -0.01f)
            {
                offset = Mathf.Lerp(offset, -offsetAmount, smoothTime * Time.deltaTime);
            }

            targetPosition.y = player.position.y;
            targetPosition.x = player.position.x + offset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);

        }
        
        #endregion
    }
}
