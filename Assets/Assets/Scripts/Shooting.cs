using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 60f;
    public bool isAutomatic = false; // Режим стрельбы

    void Update()
    {
        // Проверяем режим стрельбы
        if (isAutomatic)
        {
            // Появляется автоматическая стрельба при удерживании клавиши
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else
        {
            // Одиночная стрельба по нажатию клавиши
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        // Переключение режимов стрельбы по клавише R
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAutomatic = !isAutomatic; // Переключаем режим стрельбы
            Debug.Log("Режим стрельбы: " + (isAutomatic ? "Автоматический" : "Одиночный"));
        }
    }

    void Shoot()
    {
        // Создаем пулю из префаба
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        // Получаем компонент Rigidbody и задаем скорость пули
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
}
