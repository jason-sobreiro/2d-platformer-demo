using UnityEngine;
using Scripts.Audio;
using Scripts.Managers;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D _projectileRb;
    [SerializeField] private float _speed = 10f;
    private float _direction = 1f;
    private bool _isAimingUpward = false;
    private bool _hasCollided = false;

    private Animator _projectileAnimator;

    void Awake()
    {
        _projectileRb = GetComponent<Rigidbody2D>();
        _projectileAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        if (_projectileRb == null || _hasCollided)
        {
            return;
        }

        // Move the projectile forward
        if (_isAimingUpward)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            _projectileRb.linearVelocity = transform.right * _speed;
            return;
        }

        _projectileRb.linearVelocity = transform.right * _speed * _direction;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _hasCollided = true;
        _projectileRb.linearVelocity = Vector2.zero;
        _projectileAnimator.Play("Collision");
        AudioManager.Instance.PlayBurstSFX();
        Destroy(gameObject, 0.25f);
    }

    public void OnChangeDirection(bool isFacingRight, bool isAimingUpward)
    {
        _direction = isFacingRight ? 1f : -1f;
        _isAimingUpward = isAimingUpward;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
