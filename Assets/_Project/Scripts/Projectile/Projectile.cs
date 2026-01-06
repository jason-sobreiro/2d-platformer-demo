using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D _projectileRb;
    [SerializeField] private float _speed = 10f;
    private float _direction = 1f;
    private bool _isAimingUpward = false;

    void Awake()
    {
        _projectileRb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (_projectileRb == null)
        {
            return;
        }

        // Move the projectile forward
        if (_isAimingUpward)
        {
            transform.rotation = Quaternion.Euler(0f, 0f,90f);
            _projectileRb.linearVelocity = transform.right * _speed;
            return;
        }

        _projectileRb.linearVelocity = transform.right * _speed * _direction;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the projectile on collision with any object
        Destroy(gameObject);
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
