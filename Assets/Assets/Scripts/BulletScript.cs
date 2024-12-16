using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            bool CheckDamage = false;
            if (health != null)
            {
                health.TakeDamage(damage);
                CheckDamage = true;
            }
            Destroy(gameObject);
        }
        else
        {
            string myTag = gameObject.tag;
            string otherTag = collision.gameObject.tag;
            if (myTag != otherTag)
        {
            Destroy(gameObject);
        }
        }
    }
}
