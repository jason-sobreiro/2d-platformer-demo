using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int _maxHealth = 100;
    private int _currentHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;
        Debug.Log("Player Health initialized to " + _currentHealth);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Harmful"))
        {
            UpdateHealth(-10);
            return;
        }

        if (collision.CompareTag("HealthPickup"))
        {
            UpdateHealth(20);
            Destroy(collision.gameObject);
            return;
        }
    }
    
    void UpdateHealth(int healthChange)
    {
        _currentHealth += healthChange;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player Health updated to " + _currentHealth);
    }
}
