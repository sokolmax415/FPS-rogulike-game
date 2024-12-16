using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int bulletDamage;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<RunningEnemy>().TakeDamage(bulletDamage);
            Health health = collision.gameObject.GetComponent<Health>();
            bool CheckDamage = false;
            if (health != null)
            {
                health.TakeDamage(bulletDamage);
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
