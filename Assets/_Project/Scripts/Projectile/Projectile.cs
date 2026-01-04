using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D _projectileRb;
    [SerializeField] private float _speed = 10f;
    private float _direction = 1f;

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
        _projectileRb.linearVelocity = transform.right * _speed * _direction;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the projectile on collision with any object
        Destroy(gameObject);
    }

    public void OnChangeDirection(bool isFacingRight)
    {

        _direction = isFacingRight ? 1f : -1f;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
