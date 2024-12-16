using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
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
        animator.SetBool("Death", true);
        
    }

    
}
