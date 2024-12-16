using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //maxHealth = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Kaput!");
        Destroy(gameObject);
    }
}
