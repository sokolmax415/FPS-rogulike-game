using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 60f;
    public bool isAutomatic = false;

    void Update()
    {
        if (isAutomatic)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isAutomatic = !isAutomatic;
            Debug.Log("Режим стрельбы: " + (isAutomatic ? "Автоматический" : "Одиночный"));
        }
        // Открытие меню при нажатии на escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
}
