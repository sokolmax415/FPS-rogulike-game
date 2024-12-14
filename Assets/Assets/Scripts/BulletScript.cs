// Скрипт для пули
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Удаляем пулю при столкновении с любым объектом
        Destroy(gameObject);
    }
}
